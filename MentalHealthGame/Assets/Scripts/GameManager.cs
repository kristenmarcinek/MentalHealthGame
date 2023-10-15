using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  

    public void LoadNextLevel()
    {

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Amygdala");
    }

    public void ShowAboutMenu()
    {
        SceneManager.LoadScene("AboutMenu");
    }

    public void ShowStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

   


   

}