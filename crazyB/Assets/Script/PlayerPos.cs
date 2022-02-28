using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;

    }

    void Update()
    {
        // press 0 to destroy player
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            if (collision.collider.name == "Moving_Obstacle")
            {
                var result = Analytics.CustomEvent(
                    "Death_Reason_Obstacle_Moving"
                );
                Debug.Log(result);
            }
            else
            {
                var result = Analytics.CustomEvent(
                   "Death_Reason_Obstacle_static"
                );
                Debug.Log(result);
            }
            //record player death in analytics
            AnalyticsManager.instance.IncrementCheckpointDeaths(GameMaster.instance.lastCheckPointPos);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Debug.Log(collision.collider.name);
    }

}
