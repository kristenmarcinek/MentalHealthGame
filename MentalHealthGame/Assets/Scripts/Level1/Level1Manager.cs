using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    private Timer _timerInstance;

    private void OnEnable()
    {
        _timerInstance = FindObjectOfType<Timer>();
        _timerInstance.OnTimerIsUp += RestartLevel;
    }

    private void OnDisable()
    {
        if (_timerInstance)
        {
            _timerInstance.OnTimerIsUp -= RestartLevel;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
