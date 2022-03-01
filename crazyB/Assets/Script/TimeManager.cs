using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TimeManager instance; //singleton instance
    private bool timerGoing; //determines wether timer should be counting 
    private TimeSpan timePlaying; //used to format elapsedTime
    private float elapsedTime; //time in seconds

    //setting singleton instance
    private void Awake()
    {
        //if it is the first time creating the singleton, create it and prevent it from being destroyed while switching/loading scenes.
        //else - if the instance is accidentally created a second time, destroy that new instance.
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //timerGoing = false;
        instance.BeginTimer();
    }
    
    // start/restart timer
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
    }

    public IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            yield return null;
        }
    }

    public string TimeToString()
    {
        return timePlaying.ToString("mm':'ss");
    }
}
