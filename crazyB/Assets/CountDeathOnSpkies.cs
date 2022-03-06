using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class CountDeathOnSpkies : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            // Compute current level
            int level = 1; //SceneManager.GetActiveScene().buildIndex = 1
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                level = 2;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                level = 3;
            }
            AnalyticsResult dieOnSpikes = Analytics.CustomEvent("DieOnSpikesIn" + level);
            Debug.Log("analyticsResultDieOnSpikes: " + dieOnSpikes);
        }
    }
}
