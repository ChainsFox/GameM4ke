using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StopWatch : MonoBehaviour
{
    public bool timerActive = false;
    float currentTime;
    //float bestTime = 0;
    public Text currentTimeText;
    //public Text bestTimeText; im stuck on this rn



    void Start()
    {
        currentTime = 0;//store current time as seconds 
    }


    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
    
        }   
      
        TimeSpan time = TimeSpan.FromSeconds(currentTime);//to convert seconds into minutes/hour.
        //currentTimeText.text = currentTime.ToString();
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        
        
        
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

