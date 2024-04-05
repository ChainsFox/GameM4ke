using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSource;

    // Start is called before the first frame update
    void Start()
    {
        introSource.Play();//this will play at the start
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);//we tell the loop source to start playing, taking the current time, and adding the intro length to it
    }

}
