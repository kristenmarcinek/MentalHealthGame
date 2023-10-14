using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CortisolCollision : MonoBehaviour
{
    public Level3Manager level3Man;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            level3Man.timeRemaining -= 2f;

            Destroy(this.gameObject);
        }
    }
}
