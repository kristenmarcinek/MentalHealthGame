using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private bool _isTimerRunning = false;
    private float _currentTime = 0f;
    public float timerDuration = 5f;

    public event Action OnTimerIsUp;

    private void Update()
    {
        if (_isTimerRunning)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= timerDuration)
            {
                _currentTime = 0f;
                _isTimerRunning = false;
                OnTimerIsUp?.Invoke();
            }
        }

    }

    public void StartTimer()
    {
        _isTimerRunning = true;
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
        _currentTime = 0f;
    }
}







