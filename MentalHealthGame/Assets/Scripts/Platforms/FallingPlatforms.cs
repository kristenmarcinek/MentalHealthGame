using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private float _fallDelay = .4f;
    private float _destroyDelay = 1f;

    [SerializeField] private Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(_fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, _destroyDelay);
    }
}
