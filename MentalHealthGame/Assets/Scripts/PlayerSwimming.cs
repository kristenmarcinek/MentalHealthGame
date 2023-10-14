using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimming : MonoBehaviour
{
    private float horizontal;
    [SerializeField] float swimSpeed = 8f;
    [SerializeField] float swimForce = 8f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int maxLength = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * swimSpeed, rb.velocity.y);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxLength);
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(horizontal * swimSpeed, swimForce);
        }
    }
}
