using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatforms : MonoBehaviour
{
    public float timeToTogglePlatform = 2f;
    private float _currentTime = 0f;
    private bool _platformEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        _platformEnabled = true;
    }


    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= timeToTogglePlatform)
        {
            _currentTime = 0f;
            TogglePlatform();
        }
    }


    private void TogglePlatform()
    {
        _platformEnabled = !_platformEnabled;
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag != "Player")
            {
                child.gameObject.SetActive(_platformEnabled);
            }

        }
    }

 

}


