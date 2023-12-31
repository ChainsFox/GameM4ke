using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;

    void Start()
    {
        currentTime = startMinutes * 60;//store current time as seconds
    }


    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime < 0)
            {
                timerActive = false;
                Start();
                Debug.Log("timer finished");
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);//to convert seconds into minutes/hour.
        //currentTimeText.text = currentTime.ToString();
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

}
