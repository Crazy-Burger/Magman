using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class FinishLine : MonoBehaviour
{
    
    public GameObject completeLevelUI;
    public float restartDelay = 2f;
    private GameMaster gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            /* 
             * record time spent on the last checkpoint before finishline and upload analytics data
             */
            TimeManager.instance.EndTimer();
            AnalyticsManager.instance.RecordCheckpointTimeSpent(GameMaster.instance.lastCheckPointPos, TimeManager.instance.TimeToString());
            AnalyticsManager.instance.UploadAnalyticsData();
            Debug.LogWarning("checkpoint: " + GameMaster.instance.lastCheckPointPos.ToString());

            //restart timer for next level (for time between end of this level and the first checkpoint of next level)
            TimeManager.instance.BeginTimer();

            //CompeleteLevel();
            gm.lastCheckPointPos.x = -9;
            gm.lastCheckPointPos.y = -1.7f;
            //After build each level, go to File>>build setting, drag each scene in order
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            //Restart();
            //Invoke("Restart", restartDelay);

            int currentLevel = 1;
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                currentLevel = 2;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                currentLevel = 3;
            }
            AnalyticsResult numPlayerWin = Analytics.CustomEvent("LevelWin" + currentLevel);
            Debug.Log("analyticsResultPlayerWin: " + numPlayerWin);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompeleteLevel()
    {
        Debug.Log("LEVEL CLEAR!");
        completeLevelUI.SetActive(true);
    }
}
