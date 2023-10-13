using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCollsionDetection : MonoBehaviour
{
    private Timer _timerInstance;

    private void Awake()
    {
        _timerInstance = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _timerInstance.StartTimer();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _timerInstance.StopTimer();
        }
    }
}
