using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class WaypointFollower : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            
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
            AnalyticsResult dieOnMovingSpike = Analytics.CustomEvent("DieOnMovingSpikes" + level);
            Debug.Log("analyticsResultDieOnMoving: " + dieOnMovingSpike);
        }
    }
}
