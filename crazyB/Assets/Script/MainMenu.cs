using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void NextLevel()
    {
        //After build each level, go to File>>build setting, drag each scene in order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
