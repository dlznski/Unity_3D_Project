using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioOptions : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sliderMusic;
    public Slider sliderSounds;

    void Start()
    {
        sliderMusic.onValueChanged.AddListener(MusicVolume);
        sliderSounds.onValueChanged.AddListener(SoundsVolume);
    }

    public void MusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVol", value);
        audioMixer.SetFloat("LocationMusicVol", value);
    }

    public void SoundsVolume(float value)
    {
        audioMixer.SetFloat("SoundsVol", value);
    }
}
