using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    private bool activeCP = false;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //update time spent on last checkpoint, and restart timer 
            SoundManager.PlaySound("checkpoint");
            TimeManager.instance.EndTimer();
            AnalyticsManager.instance.RecordCheckpointTimeSpent(GameMaster.instance.lastCheckPointPos, TimeManager.instance.TimeToString());
            TimeManager.instance.BeginTimer();
            Debug.LogWarning("checkpoint: " + GameMaster.instance.lastCheckPointPos.ToString());
            gm.lastCheckPointPos = transform.position;
            activeCP = true;
        }
    }

    private void Update()
    {
        if (activeCP)
        {
            this.gameObject.SetActive(false);
        }
    }
}
