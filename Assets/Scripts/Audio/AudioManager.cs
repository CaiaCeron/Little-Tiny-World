using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.canLoop;
        }
    }

    private void Start()
    {
        PlayAudioClip("Soundtrack");
    }


    public void PlayAudioClip(string clipName)
    {
        Sound sound = Array.Find(sounds, sound => sound.clipName == clipName);
        if (sound == null)
        {
            Debug.Log("You probably wrote this variable name wrong. --> " + clipName + " <--");
            return;
        }
        sound.source.Play();
    }

    public void MasterVolumeControl(float volumeLevel)
    {
        AudioListener.volume = volumeLevel;
    }

    public void ToggleMusic(string clipName, bool state)
    {
        Sound sound = Array.Find(sounds, sound => sound.clipName == clipName);
        if (sound == null)
        {
            Debug.Log("You probably wrote this variable name wrong. --> " + clipName + " <--");
            return;
        }
        sound.source.mute = state;
    }

    public void ToggleSFX(string clipName, bool state)
    {
        Sound sound = Array.Find(sounds, sound => sound.clipName == clipName);
        if (sound == null)
        {
            Debug.Log("You probably wrote this variable name wrong. --> " + clipName + " <--");
            return;
        }
        sound.source.mute = state;
    }

}
