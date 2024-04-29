using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class OptionsSettings : MonoBehaviour
{
    public GameObject player;
    [SerializeField] FirstPersonController fpc;
    AimAssist aimAssist;

    GameObject radio;
    AudioSource musicAudioSource;
    public AudioSource musicAudioMixer;
    public AudioSource SFXAudioMixer;

    string xLookSpeedMouse;
    string yLookSpeedMouse;
    string xLookSpeedCont;
    string yLookSpeedCont;



    // Start is called before the first frame update
    public void Start()
    {
        fpc = FindObjectOfType<FirstPersonController>();
        aimAssist = FindObjectOfType<AimAssist>();
        radio = GameObject.Find("Radio");
        //musicAudioSource = radio.GetComponent<AudioSource>();
/*        if(fpc != null)
        {
            xLookSpeedMouse = fpc.mouseLookSpeedX.ToString();
            yLookSpeedMouse = fpc.mouseLookSpeedY.ToString();
            xLookSpeedCont = fpc.controllerLookSpeedX.ToString();
            yLookSpeedCont = fpc.controllerLookSpeedY.ToString();
        }*/
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
        if (fpc != null)
            fpc.mouseLookSpeedX = xSens;
        if (fpc != null)
            aimAssist.assistLookSpeedX = fpc.mouseLookSpeedX * 0.5f;
        //PlayerPrefs.SetFloat(xLookSpeedMouse, xSens);
        PlayerPrefs.SetFloat("XMouseSensitivity", xSens);
    }

    public void ChangeSensitivityYMouse(float ySens)
    {
        if (fpc != null)
            fpc.mouseLookSpeedY = ySens;
        if (fpc != null)
            aimAssist.assistLookSpeedY = fpc.mouseLookSpeedY * 0.5f;
        //PlayerPrefs.SetFloat(yLookSpeedMouse, ySens);
        PlayerPrefs.SetFloat("YMouseSensitivity", ySens);
    }

    public void ChangeSensitivityXController(float xSens)
    {
        if (fpc != null)
            fpc.controllerLookSpeedX = xSens;
        if (fpc != null)
            aimAssist.assistLookSpeedY = fpc.controllerLookSpeedX * 0.5f;
        //PlayerPrefs.SetFloat(xLookSpeedCont, xSens);
        PlayerPrefs.SetFloat("XControllerSensitivity", xSens);
    }

    public void ChangeSensitivityYController(float ySens)
    {
        if (fpc != null)
            fpc.controllerLookSpeedY = ySens;
        if (fpc != null)
            aimAssist.assistLookSpeedY = fpc.controllerLookSpeedY * 0.5f;
        //PlayerPrefs.SetFloat(yLookSpeedCont, ySens);
        PlayerPrefs.SetFloat("YControllerSensitivity", ySens);
    }
}
