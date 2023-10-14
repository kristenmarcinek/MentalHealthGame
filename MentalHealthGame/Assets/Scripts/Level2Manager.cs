using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level2Manager : MonoBehaviour
{
    public float timeRemaining = 350f;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60); 
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
                //Debug.Log(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
