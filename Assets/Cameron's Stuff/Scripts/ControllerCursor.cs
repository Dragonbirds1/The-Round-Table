using UnityEngine;
using UnityEngine.EventSystems; // For UI interaction
using UnityEngine.InputSystem; // Required for the new Input System
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;

/// <summary>
/// Moves a cursor using a gamepad or keyboard and allows UI interaction.
/// Attach this to a GameObject with a RectTransform (e.g., an Image) inside a Canvas.
/// </summary>
public class ControllerCursor : MonoBehaviour
{
    [Header("Cursor Settings")]
    public float moveSpeed = 800f; // Pixels per second
    public RectTransform canvasRect; // Reference to the parent Canvas RectTransform

    public List<PlayerInput> players = new List<PlayerInput>();
    private int currentplayers = 0;

    [Header("Input Actions")]
    public InputAction moveAction; // Vector2 input for movement
    public InputAction clickAction; // Button input for clicking

    public RectTransform[] images; // Assign 4 UI images
    public InputActionProperty[] leftSticks; // Assign 4 actions

    public RectTransform cursorRect;
    private Camera uiCamera;

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

    private void Start()
    {
        cursorRect = GetComponent<RectTransform>();

        // Get the camera rendering the UI
        Canvas canvas = canvasRect.GetComponent<Canvas>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        // Start cursor in center
        cursorRect.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        MoveCursor();
        HandleClick();
    }

    bool Pressed(PlayerInput player)
    {
        var device = player.devices[0];

        if (device is Gamepad pad)
            return pad.rightStick.ReadValue() != Vector2.zero;

        if (device is Keyboard keyboard)
            return keyboard.spaceKey.wasPressedThisFrame;

        return false;
    }

    private void MoveCursor()
    {
        for (int i = 0; i < 1; i++)
        {
            Vector2 move = leftSticks[i].action.ReadValue<Vector2>();
            images[i].anchoredPosition += move * moveSpeed * Time.deltaTime;
        }
    }

    private void HandleClick()
    {
        if (clickAction.WasPressedThisFrame())
        {
            // Simulate a UI click at the cursor position
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = RectTransformUtility.WorldToScreenPoint(uiCamera, cursorRect.position)
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}