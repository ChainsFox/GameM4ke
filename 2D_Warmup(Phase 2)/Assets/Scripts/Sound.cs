using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //show up on unity editor
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool loop;

    public AudioMixerGroup Output;

    [HideInInspector]
    public AudioSource source;
}
