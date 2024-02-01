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
        //musicAudioSource = radio.GetComponent<AudioSource>();

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

    public void ChangeSensitivityXMouse(float xSens)
    {
        fpc.mouseLookSpeedX = xSens;
    }

    public void ChangeSensitivityYMouse(float ySens)
    {
        fpc.mouseLookSpeedY = ySens;
    }

    public void ChangeSensitivityXController(float xSens)
    {
        fpc.controllerLookSpeedX = xSens;
    }

    public void ChangeSensitivityYController(float ySens)
    {
        fpc.controllerLookSpeedY = ySens;
    }
}
