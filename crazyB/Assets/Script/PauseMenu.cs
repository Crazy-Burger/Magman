using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Debug.Log("Restart Pressed...");
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene == "TutorialLevel")
        {
            GameMaster.instance.lastCheckPointPos.x = -9f;
            GameMaster.instance.lastCheckPointPos.y = -1.5f;
        }
        else
        {
            GameMaster.instance.lastCheckPointPos.x = -9f;
            GameMaster.instance.lastCheckPointPos.y = -1.7f;
        }
        SceneManager.LoadScene(currentScene);
        Resume();
    }

    public void Quit()
    {
        Debug.Log("Quit Clicked...");
        Application.Quit();
    }
}
