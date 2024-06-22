using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot locationSnapshot;

    void Start()
    {
        normalSnapshot.TransitionTo(0.01f);
    }

    public void SetNormalAudio()
    {
        Debug.Log("Switching to normal Audio");
        normalSnapshot.TransitionTo(0.5f);
    }

    public void SetPausedAudio()
    {
        pausedSnapshot.TransitionTo(0.5f);

    }

    public void SetLocationAudio()
    {
        Debug.Log("Switching to Location Audio");
        locationSnapshot.TransitionTo(0.5f);
    }

}
