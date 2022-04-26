using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Compute current level
        int level = 1;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            level = 2;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            level = 3;
        }
        AnalyticsResult numPlayerIn = Analytics.CustomEvent("PlayLevel" + level);
        Debug.Log("Current level: " + level);
        Debug.Log("analyticsResult Number of Player In: " + numPlayerIn);
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
    public void SelectLevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void SelectLevel2()
    {
        SceneManager.LoadScene(3);
    }
    public void SelectLevel3()
    {
        SceneManager.LoadScene(5);
    }
    public void SelectLevel4()
    {
        SceneManager.LoadScene(7);
    }
}
