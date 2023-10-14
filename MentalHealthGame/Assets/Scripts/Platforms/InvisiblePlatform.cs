using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour
{
    private bool _isVisible;
    private SpriteRenderer _spriteRend;

    // Start is called before the first frame update
    void Start()
    {
       
        _spriteRend = gameObject.GetComponent<SpriteRenderer>();
       this. _spriteRend.enabled = false;
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this._spriteRend.enabled = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           this. _spriteRend.enabled = false;
        }
    }
}
