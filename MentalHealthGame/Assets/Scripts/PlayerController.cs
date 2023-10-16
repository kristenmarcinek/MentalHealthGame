using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float doubleJumpForce = 12f;
    [SerializeField] private int maxLength = 50;

    private float horizontal;
    private bool isFacingRight = true;
    private bool doubleJump;
    private bool isJumping;

    [Header("Components")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public SpriteRenderer sR;
    public Animator animator;

    [Header("Control Variables")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    [Header("Running Jump Variables")]
    [SerializeField] private float releaseJumpDuration = 0.2f;
    private float releaseJumpCounter;

    private void Update()
    {
        GetInput();
        CheckGrounded();
        HandleJumpInput();
        HandleDoubleJump();
        HandleReleaseJumpInput();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        MoveHorizontally();
        AdjustGravity();
        LimitVelocity();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void CheckGrounded()
    {
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            releaseJumpCounter = 0f;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
            releaseJumpCounter += Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            Jump(jumpForce);
        }
    }

    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        jumpBufferCounter = 0f;
        StartCoroutine(JumpCooldown());
    }

    private void HandleDoubleJump()
    {
        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            animator.SetBool("isJumping", false);
        }

        if ((Input.GetButtonDown("Jump") && isGrounded()) || (Input.GetButtonDown("Jump") && doubleJump))
        {
            Jump(doubleJumpForce);
            doubleJump = !doubleJump;
            animator.SetBool("isJumping", true);
        }
    }

    private void HandleReleaseJumpInput()
    {
        if (Input.GetButtonUp("Jump") && (rb.velocity.y > 0f || rb.velocity.y < 0f) && releaseJumpCounter < releaseJumpDuration)
        {
            // Reduce upward velocity when jump button is released
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isRunning", Mathf.Abs(horizontal) > 0 && isGrounded());
        FlipCharacter();
    }

    private void MoveHorizontally()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void AdjustGravity()
    {
        rb.gravityScale = (rb.velocity.y < -0.1) ? rb.gravityScale * 1.1f : 2;
    }

    private void LimitVelocity()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FlipCharacter()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            sR.flipX = !sR.flipX;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        animator.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
        animator.SetBool("isJumping", false);
    }
}
