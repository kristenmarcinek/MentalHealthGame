using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSwimming : MonoBehaviour
{
    private float horizontal;
    [SerializeField] float swimSpeed = 8f;
    [SerializeField] float swimForce = 8f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int maxLength = 50;

    [SerializeField] List<Transform> groundChecks;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private Animator animator;
    private bool isFacingRight = true;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(horizontal * swimSpeed, rb.velocity.y);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, swimForce);
        }

        animator.SetBool("isSwimming", Mathf.Abs(horizontal) > 0);

        if (isGrounded())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Left();
    }

    public bool isGrounded()
    {
        foreach (Transform groundCheck in groundChecks)
        {
            if (Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    private void Left()
    {
        // Flip the character's sprite and handle speed
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            sR.flipX = !sR.flipX;
        }

        //animator.SetFloat("speed", Mathf.Abs(horizontal));
    }
}



