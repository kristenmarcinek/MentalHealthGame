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
    private bool doubleJump;
    [SerializeField] int maxLength = 50;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    // [SerializeField] SpriteRenderer sR;
    // [SerializeField] Animator animator;

    private bool isJumping;
    [SerializeField] float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

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
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded() || Input.GetButtonDown("Jump") && doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            doubleJump = !doubleJump;
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.x > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Left();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (rb.velocity.y < -0.1)
        {
            ChangeGravity();
        } else {
            rb.gravityScale = 2;
        }

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
    }

    public void Left()
    {
        // || !isFacingRight && horizontal > 0f
        if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            // sR.flipX = true;
        }

        // || isFacingRight && horizontal > 0f
        if (!isFacingRight && horizontal > 0f)
        {
            isFacingRight = true;
            // sR.flipX = false;
        }

        //animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void ChangeGravity()
    {
        rb.gravityScale = rb.gravityScale * 1.1f;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}