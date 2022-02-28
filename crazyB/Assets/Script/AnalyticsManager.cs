using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    public Dictionary<string, int> checkpointDeaths;
    public Dictionary<string, object> checkpointTimeSpent;
    public Dictionary<string, int> ekeyUsageTimes;
    public Vector2[] checkpoints = { new Vector2(-9.0f, -1.0f), new Vector2(8.0f, -1.0f), new Vector2(20.0f, -1.0f) };
    private void Awake()
    {
        //if it is the first time creating the singleton, create it and prevent it from being destroyed while switching/loading scenes.
        //else - if the instance is accidentally created a second time, destroy that new instance.
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            //initialize analytics data
            checkpointDeaths = new Dictionary<string, int>()
            {
                {"checkpoint1", 0 },
                {"checkpoint2", 0 },
                {"checkpoint3", 0 }
            };
            checkpointTimeSpent = new Dictionary<string, object>()
            {
                {"checkpoint1", "0" },
                {"checkpoint2", "0" },
                {"checkpoint3", "0" }
            };
            ekeyUsageTimes = new Dictionary<string, int>()
            {
                {"checkpoint1", 0 },
                {"checkpoint2", 0 },
                {"checkpoint3", 0 }
            };

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IncrementCheckpointDeaths(Vector2 checkpointPosition)
    {
        if(checkpointPosition == checkpoints[0]) // checkpoint1
        {
            checkpointDeaths["checkpoint1"] += 1;
        }
        else if(checkpointPosition == checkpoints[1])// checkpoint2
        {
            checkpointDeaths["checkpoint2"] += 1;
        }
        else if (checkpointPosition == checkpoints[2])//checkpoint3
        {
            checkpointDeaths["checkpoint3"] += 1;
        }
        else
        {
            //should not happen
            Debug.LogError("received wrong checkpoint position in analytics. please check if you have changed the position of checkpoints. if yes, you need to change the values of checkpoints[] accordingly.");
        }
    }
    public void IncrementEkeyUsageTimes(Vector2 checkpointPosition)
    {
        if (checkpointPosition == checkpoints[0]) // checkpoint1
        {
            ekeyUsageTimes["checkpoint1"] += 1;
        }
        else if (checkpointPosition == checkpoints[1])// checkpoint2
        {
            ekeyUsageTimes["checkpoint2"] += 1;
        }
        else if (checkpointPosition == checkpoints[2])//checkpoint3
        {
            ekeyUsageTimes["checkpoint3"] += 1;
        }
        else
        {
            //should not happen
            Debug.LogError("received wrong checkpoint position in analytics. please check if you have changed the position of checkpoints. if yes, you need to change the values of checkpoints[] accordingly.");
        }
    }
    public void RecordCheckpointTimeSpent(Vector2 checkpointPosition, string time)
    {
        if (checkpointPosition == checkpoints[0]) // checkpoint1
        {
            checkpointTimeSpent["checkpoint1"] = time;
        }
        else if (checkpointPosition == checkpoints[1])// checkpoint2
        {
            checkpointTimeSpent["checkpoint2"] = time;
        }
        else if (checkpointPosition == checkpoints[2])//checkpoint3
        {
            checkpointTimeSpent["checkpoint3"] = time;
        }
        else
        {
            //should not happen
            Debug.LogError("received wrong checkpoint position in analytics. please check if you have changed the position of checkpoints. if yes, you need to change the values of checkpoints[] accordingly.");
        }
    }
    public void UploadAnalyticsData()
    {
        //send checkpoint deaths and time spent on each checkpoint seperately as custom events.
        var result = Analytics.CustomEvent(
                "Checkpoint Deaths",
                new Dictionary<string, object>
                {
                    {"checkpoint1", checkpointDeaths["checkpoint1"] },
                    {"checkpoint2", checkpointDeaths["checkpoint2"] },
                    {"checkpoint3", checkpointDeaths["checkpoint3"] }
                }
            );
        Debug.Log(result);
        result = Analytics.CustomEvent(
                "Ekey Usage Times",
                new Dictionary<string, object>
                {
                    {"checkpoint1", ekeyUsageTimes["checkpoint1"] },
                    {"checkpoint2", ekeyUsageTimes["checkpoint2"] },
                    {"checkpoint3", ekeyUsageTimes["checkpoint3"] }
                }
            );
        Debug.Log("ekey: " + result);
        result = Analytics.CustomEvent(
                "Time Spent on Checkpoints",
                checkpointTimeSpent
            );
        Debug.Log(result);
    }
}
