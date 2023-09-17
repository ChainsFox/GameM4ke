using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake() //awake is a lot like start method, instead it call right before.
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;   
            s.source.loop = s.loop;
            
        }
    }

    void Start ()
    {   
        //Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //sounds array, where sound.name = name
        s.source.Play();
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

    }

    public void Disable(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //sounds array, where sound.name = name
        s.source.enabled = false;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

    }

    public void Enable(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //sounds array, where sound.name = name
        s.source.enabled = true;
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

    }


}
