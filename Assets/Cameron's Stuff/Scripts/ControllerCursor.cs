using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
public class ControllerCursor : MonoBehaviour
{
    public float moveSpeed = 800f;
    public RectTransform canvasRect;

    private RectTransform cursorRect;
    private Camera uiCamera;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction clickAction;

    //------------------------------------------------

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // These MUST match your Input Actions asset
        moveAction = playerInput.actions["Move"];
        clickAction = playerInput.actions["Click"];

        cursorRect = GetComponent<RectTransform>();

        Canvas canvas = canvasRect.GetComponent<Canvas>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null
            : canvas.worldCamera;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        clickAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        clickAction.Disable();
    }

    //------------------------------------------------

    private void Update()
    {
        MoveCursor();
        HandleClick();
    }

    //------------------------------------------------

    private void MoveCursor()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector2 newPos =
            cursorRect.anchoredPosition +
            moveInput * moveSpeed * Time.deltaTime;

        Vector2 canvasSize = canvasRect.sizeDelta;

        newPos.x = Mathf.Clamp(newPos.x, -canvasSize.x / 2, canvasSize.x / 2);
        newPos.y = Mathf.Clamp(newPos.y, -canvasSize.y / 2, canvasSize.y / 2);

        cursorRect.anchoredPosition = newPos;
    }

    //------------------------------------------------

    private void HandleClick()
    {
        if (!clickAction.WasPressedThisFrame())
            return;

        PointerEventData pointerData =
            new PointerEventData(EventSystem.current)
            {
                position = RectTransformUtility.WorldToScreenPoint(
                    uiCamera,
                    cursorRect.position)
            };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            ExecuteEvents.Execute(
                result.gameObject,
                pointerData,
                ExecuteEvents.pointerClickHandler);
        }
    }
}
