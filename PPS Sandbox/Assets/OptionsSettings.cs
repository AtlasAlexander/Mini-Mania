using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsSettings : MonoBehaviour
{
    public GameObject player;
    FirstPersonController fpc;

    public AudioMixer mainAudioMixer;
    public AudioMixer musicAudioMixer;
    public AudioMixer SFXAudioMixer;


    // Start is called before the first frame update
    public void Start()
    {
        fpc = FindObjectOfType<FirstPersonController>();
    }

    public void InvertLook()
    {
        fpc.invertLook = !fpc.invertLook;
    }

    public void ChangeSensitivityX(float xSens)
    {
        fpc.lookSpeedX = xSens;
    }

    public void ChangeSensitivityY(float ySens)
    {
        fpc.lookSpeedY = ySens;
    }

    public void SetMainVolume(float MainVolume)
    {
        mainAudioMixer.SetFloat("volume", MainVolume);
    }

    public void SetMusicVolume(float MusicVolume)
    {
        musicAudioMixer.SetFloat("volume", MusicVolume);
    }

    public void SetSFXVolume(float SFXVolume)
    {
        SFXAudioMixer.SetFloat("volume", SFXVolume);
    }
}
