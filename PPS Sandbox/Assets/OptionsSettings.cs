using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsSettings : MonoBehaviour
{
    public GameObject player;
    FirstPersonController fpc;

    GameObject radio;
    AudioSource musicAudioSource;
    public AudioSource musicAudioMixer;
    public AudioSource SFXAudioMixer;


    // Start is called before the first frame update
    public void Start()
    {
        fpc = FindObjectOfType<FirstPersonController>();
        radio = GameObject.Find("Radio");
        musicAudioSource = radio.GetComponent<AudioSource>();

    }

    public void InvertLook(bool tickOn)
    {
        if (tickOn)
        {
            fpc.invertLook = true;
        }
        else
        {
            fpc.invertLook = false;
        }
    }

    public void ChangeSensitivityX(float xSens)
    {
        fpc.lookSpeedX = xSens;
    }

    public void ChangeSensitivityY(float ySens)
    {
        fpc.lookSpeedY = ySens;
    }
}
