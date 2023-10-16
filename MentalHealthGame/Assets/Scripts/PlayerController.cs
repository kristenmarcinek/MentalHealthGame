using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpForce = 16f;
    private bool isFacingRight = true;
    private bool canDoubleJump;
    [SerializeField] int maxLength = 50;
    [SerializeField] float runningJump = 0.6f;
    [SerializeField] float doubleJumpForce = 12f; // Adjust the double jump height here

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private bool isJumping;
    [SerializeField] float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool jumpButtonPressed;

    [SerializeField] SpriteRenderer sR;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

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

        if(isGrounded())
        {
            animator.SetBool("isJumping", false);
        }

        if(!isGrounded())
        {
            animator.SetBool("isJumping", true);
        }

        float currentJumpForce = jumpForce;

        if (!isGrounded() && jumpButtonPressed)
        {
            currentJumpForce = jumpForce;
        }
        else
        {
            currentJumpForce = jumpForce * runningJump;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (isGrounded() || canDoubleJump))
        {
            float jumpHeight = isGrounded() ? jumpForce : doubleJumpForce; // Reduce the double jump height here
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);

            canDoubleJump = !canDoubleJump;
        }

        Left();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) && isGrounded())
        {
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) && isGrounded())
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (rb.velocity.y < -0.1)
        {
            ChangeGravity();
        }
        else
        {
            rb.gravityScale = 2;
        }

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
    }

    public void Left()
    {
        if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            sR.flipX = true;
        }

        if (!isFacingRight && horizontal > 0f)
        {
            isFacingRight = true;
            sR.flipX = false;
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    public void ChangeGravity()
    {
        rb.gravityScale = rb.gravityScale * 1.045f;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}
