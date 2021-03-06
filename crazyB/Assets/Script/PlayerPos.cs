using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;
    // Start is called before the first frame update
    public Rigidbody2D rb;

    IEnumerator ExecuteAfterTime()
    {
        Time.timeScale = .0000001f;
        yield return new WaitForSeconds(1 * Time.timeScale);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

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
        if(rb.position.y < -50f)
        {
            // Compute current level
            int level = 1;//SceneManager.GetActiveScene().buildIndex = 1
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                level = 2;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                level = 3;
            }
            AnalyticsResult dieOnFalling = Analytics.CustomEvent("DieOnFalling" + level);
            Debug.Log("analyticsResultDieOnFalling: " + dieOnFalling);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {

            SoundManager.PlaySound("death");
            StartCoroutine(ExecuteAfterTime());
            
            if (collision.collider.name == "Moving_Obstacle")
            {
                var result = Analytics.CustomEvent(
                    "Death_Reason_Obstacle_Moving"
                );
                // Debug.Log("moving obstacle");
                // Debug.Log(result);
            }
            else
            {
                var result = Analytics.CustomEvent(
                   "Death_Reason_Obstacle_static"
                );
                // Debug.Log("static obstacle");
                // Debug.Log(result);
            }
            //record player death in analytics
            AnalyticsManager.instance.IncrementCheckpointDeaths(GameMaster.instance.lastCheckPointPos);
        }
        
        // Debug.Log(collision.collider.name);
    }

}
