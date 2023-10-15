using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CortisolCollision : MonoBehaviour
{

    public Level3Manager level3Man;
    public float speed = 2.0f; // Adjust the speed as needed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; 
        rb.velocity = (gameObject.CompareTag("CortisolUp")) ? Vector2.up * speed : Vector2.down * speed;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            level3Man.timeRemaining -= 2f;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
           
            rb.velocity = -rb.velocity; // Invert velocity to change direction
        }
    }
}




