using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CortisolCollision : MonoBehaviour
{
    public Level3Manager level3Man;
    public bool isUp;
    //public bool isDown;
    public float floatSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "CortisolUp")
        {
            isUp = true;
        }

        if(this.gameObject.tag == "CortisolDown")
        {
            //isDown = true;
            isUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            transform.position += new Vector3(0, floatSpeed, 0);
        }

        if(!isUp)
        {
            transform.position -= new Vector3(0, floatSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            level3Man.timeRemaining -= 2f;

            Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Ground") && isUp)
        {
            Debug.Log("colliding up");
            isUp = false;
            //isDown = true;
        }

        if(other.gameObject.CompareTag("Ground") && !isUp)
        {
            Debug.Log("colliding down");
            isUp = true;
            //isDown = false;
        }
    }
}
