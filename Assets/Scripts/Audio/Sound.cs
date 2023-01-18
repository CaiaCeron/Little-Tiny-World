using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    public string clipName;

    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(-3.0f, 3.0f)]
    public float pitch = 1.0f;

    public bool canLoop = false;
}
