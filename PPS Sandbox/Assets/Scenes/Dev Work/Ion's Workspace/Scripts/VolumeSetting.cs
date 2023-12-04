using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{

    public AudioMixer mainAudioMixer;
    public AudioMixer musicAudioMixer;
    public AudioMixer SFXAudioMixer;
    public void SetMainVolume (float volume)
    {
        mainAudioMixer.SetFloat("volume", volume);
    }

    public void SetMusicVolume (float volume)
    {
        musicAudioMixer.SetFloat("volume", volume);
    }

    public void SetSFXVolume (float volume)
    {
        SFXAudioMixer.SetFloat("volume", volume);
    }

    public void Mute()
    {

    }

}
