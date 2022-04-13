using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHandler : MonoBehaviour
{
    public void LoadLevel()
    {
        int id = 1;

        if (PlayerPrefs.HasKey("Level"))
        {
            id = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", id);
        }

        SceneManager.LoadScene($"Level {id}");
    }
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
