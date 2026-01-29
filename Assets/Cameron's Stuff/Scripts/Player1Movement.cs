using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Movement : MonoBehaviour
{
    /// <summary>
    /// This script will handle the movement of Player 1.
    /// </summary>

    [Header("Player 1 Movement Settings")]
    public float moveSpeed = 5f;
    public float currentSpeed = 0;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
        animator.SetFloat("X", movement.x);
        animator.SetFloat("Y", movement.y);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}
