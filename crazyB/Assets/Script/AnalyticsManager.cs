using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    public Dictionary<string, int> checkpointDeaths;
    public Dictionary<string, object> checkpointTimeSpent;
    public Dictionary<string, int> checkpointJumps;
    public Dictionary<string, int> ekeyUsageTimes;

    public Vector2[] checkpoints = { new Vector2(-9f, -1.5f), new Vector2(23.1f, -1.2f), new Vector2(66.3f, -1.2f), new Vector2(-9f, -1.7f), new Vector2(14.3f, -1.69f), new Vector2(56.62f, -1.25f), new Vector2(-6.74f, -10.32f), new Vector2(30.59f, -22.49f) };

    private bool debug = false;
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
                {"checkpoint3", 0 },
                {"checkpoint4", 0 },
                {"checkpoint5", 0 },
                {"checkpoint6", 0 },
                {"checkpoint7", 0 },
                {"checkpoint8", 0 }

            };
            checkpointTimeSpent = new Dictionary<string, object>()
            {
                {"checkpoint1", "0:00" },
                {"checkpoint2", "0:00" },
                {"checkpoint3", "0:00" },
                {"checkpoint4", "0:00" },
                {"checkpoint5", "0:00" },
                {"checkpoint6", "0:00" },
                {"checkpoint7", "0:00" },
                {"checkpoint8", "0:00" }
            };
            checkpointJumps = new Dictionary<string, int>()
            {
                {"checkpoint1", 0 },
                {"checkpoint2", 0 },
                {"checkpoint3", 0 },
                {"checkpoint4", 0 },
                {"checkpoint5", 0 },
                {"checkpoint6", 0 },
                {"checkpoint7", 0 },
                {"checkpoint8", 0 }
            };
            ekeyUsageTimes = new Dictionary<string, int>()
            {
                {"checkpoint1", 0 },
                {"checkpoint2", 0 },
                {"checkpoint3", 0 },
                {"checkpoint4", 0 },
                {"checkpoint5", 0 },
                {"checkpoint6", 0 },
                {"checkpoint7", 0 },
                {"checkpoint8", 0 }
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IncrementCheckpointDeaths(Vector2 checkpointPosition)
    {
        if (checkpointPosition == checkpoints[0]) 
        {
            checkpointDeaths["checkpoint1"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint1 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[1])
        {
            checkpointDeaths["checkpoint2"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint2 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[2])
        {
            checkpointDeaths["checkpoint3"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint3 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[3])
        {
            checkpointDeaths["checkpoint4"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint4 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[4])
        {
            checkpointDeaths["checkpoint5"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint5 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[5])
        {
            checkpointDeaths["checkpoint6"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint6 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[6])
        {
            checkpointDeaths["checkpoint7"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint7 deaths + 1");
            }
        }
        else if (checkpointPosition == checkpoints[7])
        {
            checkpointDeaths["checkpoint8"] += 1;
            if (debug)
            {
                Debug.LogWarning("checkpoint8 deaths + 1");
            }
        }
        else
        {
            //should not happen
            Debug.LogWarning("received wrong checkpoint position in analytics. please check if you have changed the position of checkpoints. if yes, you need to change the values of checkpoints[] accordingly.");
        }
    }
    public void RecordCheckpointTimeSpent(Vector2 checkpointPosition, string time)
    {
        if (checkpointPosition == checkpoints[0]) 
        {
            checkpointTimeSpent["checkpoint1"] = time;
        }
        else if (checkpointPosition == checkpoints[1])
        {
            checkpointTimeSpent["checkpoint2"] = time;
        }
        else if (checkpointPosition == checkpoints[2])
        {
            checkpointTimeSpent["checkpoint3"] = time;
        }
        else if (checkpointPosition == checkpoints[3])
        {
            checkpointTimeSpent["checkpoint4"] = time;
        }
        else if (checkpointPosition == checkpoints[4])
        {
            checkpointTimeSpent["checkpoint5"] = time;
        }
        else if (checkpointPosition == checkpoints[5])
        {
            checkpointTimeSpent["checkpoint6"] = time;
        }
        else if (checkpointPosition == checkpoints[6])
        {
            checkpointTimeSpent["checkpoint7"] = time;
        }
        else if (checkpointPosition == checkpoints[7])
        {
            checkpointTimeSpent["checkpoint8"] = time;
        }
        else
        {
            //should not happen
            Debug.LogWarning("received wrong checkpoint position in analytics. please check if you have changed the position of checkpoints. if yes, you need to change the values of checkpoints[] accordingly.");
        }
    }

    public void IncrementCheckpointJumps(Vector2 checkpointPosition)
    {
        Debug.Log(checkpointPosition);

        if (checkpointPosition == checkpoints[0]) 
        {
            checkpointJumps["checkpoint1"] += 1;
        }
        else if (checkpointPosition == checkpoints[1])
        {
            checkpointJumps["checkpoint2"] += 1;
        }
        else if (checkpointPosition == checkpoints[2])
        {
            checkpointJumps["checkpoint3"] += 1;
        }
        else if (checkpointPosition == checkpoints[3])
        {
            checkpointJumps["checkpoint4"] += 1;
        }
        else if (checkpointPosition == checkpoints[4])
        {
            checkpointJumps["checkpoint5"] += 1;
        }
        else if (checkpointPosition == checkpoints[5])
        {
            checkpointJumps["checkpoint6"] += 1;
        }
        else if (checkpointPosition == checkpoints[6])
        {
            checkpointJumps["checkpoint7"] += 1;
        }
        else if (checkpointPosition == checkpoints[7])
        {
            checkpointJumps["checkpoint8"] += 1;
        }
        else
        {
            //should not happen
            Debug.LogError("checkpointJumps error.");
        }
    }

    // E key analytics - increase the usage times of e key
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
        else if (checkpointPosition == checkpoints[3])// checkpoint2
        {
            ekeyUsageTimes["checkpoint4"] += 1;
        }
        else if (checkpointPosition == checkpoints[4])//checkpoint3
        {
            ekeyUsageTimes["checkpoint5"] += 1;
        }
        else if (checkpointPosition == checkpoints[5])// checkpoint2
        {
            ekeyUsageTimes["checkpoint6"] += 1;
        }
        else if (checkpointPosition == checkpoints[6])//checkpoint3
        {
            ekeyUsageTimes["checkpoint7"] += 1;
        }
        else if (checkpointPosition == checkpoints[7])//checkpoint3
        {
            ekeyUsageTimes["checkpoint8"] += 1;
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
                    {"checkpoint3", checkpointDeaths["checkpoint3"] },
                    {"checkpoint4", checkpointDeaths["checkpoint4"] },
                    {"checkpoint5", checkpointDeaths["checkpoint5"] },
                    {"checkpoint6", checkpointDeaths["checkpoint6"] },
                    {"checkpoint7", checkpointDeaths["checkpoint7"] },
                    {"checkpoint8", checkpointDeaths["checkpoint8"] }
                }
            );
        Debug.Log(result);
        result = Analytics.CustomEvent(
                "Checkpoint Jumps",
                new Dictionary<string, object>
                {
                    {"checkpoint1", checkpointJumps["checkpoint1"] },
                    {"checkpoint2", checkpointJumps["checkpoint2"] },
                    {"checkpoint3", checkpointJumps["checkpoint3"] },
                    {"checkpoint4", checkpointJumps["checkpoint4"] },
                    {"checkpoint5", checkpointJumps["checkpoint5"] },
                    {"checkpoint6", checkpointJumps["checkpoint6"] },
                    {"checkpoint7", checkpointJumps["checkpoint7"] },
                    {"checkpoint8", checkpointJumps["checkpoint8"] }
                }
            );
        Debug.Log("checkpoint jumps: " + result);
        result = Analytics.CustomEvent(
               "Ekey Usage Times",
               new Dictionary<string, object>
               {
                    {"checkpoint1", ekeyUsageTimes["checkpoint1"] },
                    {"checkpoint2", ekeyUsageTimes["checkpoint2"] },
                    {"checkpoint3", ekeyUsageTimes["checkpoint3"] },
                    {"checkpoint4", ekeyUsageTimes["checkpoint4"] },
                    {"checkpoint5", ekeyUsageTimes["checkpoint5"] },
                    {"checkpoint6", ekeyUsageTimes["checkpoint6"] },
                    {"checkpoint7", ekeyUsageTimes["checkpoint7"] },
                    {"checkpoint8", ekeyUsageTimes["checkpoint8"] }
               }
           );
        Debug.Log("ekey analytics result: " + result);
        result = Analytics.CustomEvent(
                "Time Spent on Checkpoints",
                checkpointTimeSpent
            );
        Debug.Log(result);
    }
}
