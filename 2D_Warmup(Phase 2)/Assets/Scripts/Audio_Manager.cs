using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance;
    //public GameManagerScript GM;
    //public SettingsMenu SM;


    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake ()
    {
        if (Instance == null)
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start ()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    //To call sounds use: Audio_Manager.Instance.PlaySFX("JumpSFX");
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        //SM.SaveMusicVolume();
        //GM.SaveMusicVolume();
    }

     public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        //SM.SaveSfxVolume();
        //GM.SaveSfxVolume();
        
    }


    public void DisableSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
        {
            s.source.enabled = false;
        }

    }

    //public void EnableSFX(string name)
    //{
    //    Sound s = Array.Find(sfxSounds, x => x.name == name);

    //    if (s == null)
    //    {
    //        Debug.Log("Sound not found");
    //    }

    //    else
    //    {
    //        s.source.enabled = true; //disable work but this doesnt lol, still accomplish my purpose for it tho
    //    } 

    //}



}
