using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float doubleJumpForce = 12f;
    [SerializeField] private int maxLength = 50;
    [SerializeField] private float runningJump = 0.6f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isFacingRight = true;
    private bool canDoubleJump;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool jumpButtonPressed;
    private bool isJumping;
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private Animator animator;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if the player is grounded and update coyoteTimeCounter
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true; // Reset double jump when grounded
            if (isJumping)
            {
                isJumping = false; // Reset isJumping when grounded
                animator.SetBool("isJumping", false); // Set jump animation parameter
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Check for jump input and handle jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            jumpButtonPressed = true;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            jumpButtonPressed = false;
        }

        // Handle jump logic
        if (jumpBufferCounter > 0f)
        {
            TryJump();
        }

        // Handle running animation
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0 && isGrounded());

        // Handle flip and speed for left/right movement
        Left();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Handle gravity scaling
        if (rb.velocity.y < -0.1f)
        {
            rb.gravityScale = 2.0f;
        }
        else
        {
            rb.gravityScale = 2.0f;
        }

        // Clamp maximum velocity
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
    }

    private void TryJump()
    {
        if (isGrounded())
        {
            // Player is on the ground, perform a regular jump
            Jump(jumpForce);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            // Player can perform a double jump
            Jump(doubleJumpForce);
            canDoubleJump = false;
        }
    }

    private void Jump(float jumpHeight)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        isJumping = true;
        animator.SetBool("isJumping", true);
    }

    private void Left()
    {
        // Flip the character's sprite and handle speed
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            sR.flipX = !sR.flipX;
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private bool isGrounded()
    {
        // Check if the player is grounded based on the ground layer
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
