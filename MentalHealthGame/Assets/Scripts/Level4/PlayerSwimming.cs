using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSwimming : MonoBehaviour
{
    private float horizontal;
    [SerializeField] float swimSpeed = 8f;
    [SerializeField] float swimForce = 8f;
    [SerializeField] int maxLength = 50;
    private bool isFacingRight = true;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private Animator animator;




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

        Left();
    }



    private void Left()
    {
        // Flip the character's sprite and handle speed
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            sR.flipX = !sR.flipX;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}



