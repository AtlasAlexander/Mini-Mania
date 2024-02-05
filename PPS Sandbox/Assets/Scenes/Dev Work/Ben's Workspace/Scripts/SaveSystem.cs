using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] GameObject PauseOptionMenu;
    GameObject CheckpointsOBJ;

    Slider MusicSlider;
    Slider SFXSlider;
    Slider XControllersensSlider;
    Slider YControllersensSlider;
    Slider XMousesensSlider;
    Slider YMousesensSlider;
    Toggle MusicMuteToggle;
    Toggle SFXMuteToggle;
    Toggle InvertLookToggle;


    bool invertLook;
    bool SFXMute;

    // Start is called before the first frame update
    void Start()
    {
        CheckpointsOBJ = GameObject.Find("Checkpoints");

        MusicSlider = PauseOptionMenu.transform.Find("Music VolumeSlider").GetComponent<Slider>();
        SFXSlider = PauseOptionMenu.transform.Find("SFX VolumeSlider").GetComponent<Slider>();
        XControllersensSlider = PauseOptionMenu.transform.Find("XSens Slider - Controller").GetComponent<Slider>();
        YControllersensSlider = PauseOptionMenu.transform.Find("YSens Slider - Controller").GetComponent<Slider>();
        XMousesensSlider = PauseOptionMenu.transform.Find("XSens Slider").GetComponent<Slider>();
        YMousesensSlider = PauseOptionMenu.transform.Find("YSens Slider - Controller").GetComponent<Slider>();
        MusicMuteToggle = PauseOptionMenu.transform.Find("MusicToggle").GetComponent<Toggle>();
        SFXMuteToggle = PauseOptionMenu.transform.Find("SFX Toggle").GetComponent<Toggle>();
        InvertLookToggle = PauseOptionMenu.transform.Find("InvertToggle").GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("MusicVolume"))
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("SFXVolume"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

/*        if (PlayerPrefs.HasKey("XMouseSensitivity"))
            XMousesensSlider.value = PlayerPrefs.GetFloat("XMouseSensitivity");
        if (PlayerPrefs.HasKey("YMouseSensitivity"))
            YMousesensSlider.value = PlayerPrefs.GetFloat("YMouseSensitivity");
        if (PlayerPrefs.HasKey("XControllerSensitivity"))
            XControllersensSlider.value = PlayerPrefs.GetFloat("XControllerSensitivity");
        if (PlayerPrefs.HasKey("YControllerSensitivity"))
            YControllersensSlider.value = PlayerPrefs.GetFloat("YControllerSensitivity");*/

/*        if (PlayerPrefs.HasKey("InvertLook"))
        {
            if (PlayerPrefs.GetInt("InvertLook") == 1)
                invertLook = true;
            else
                invertLook = false;
            InvertLookToggle.isOn = invertLook;
        }*/

        if (PlayerPrefs.HasKey("MusicMute"))
        {
            if (PlayerPrefs.GetInt("MusicMute") == 1)
                MusicMuteToggle.isOn = true;
            else
                MusicMuteToggle.isOn = false;
        }
        if (PlayerPrefs.HasKey("SFXMute"))
        {
            if (PlayerPrefs.GetInt("SFXMute") == 1)
                SFXMute = true;
            else
                SFXMute = false;
            SFXMuteToggle.isOn = SFXMute;
        }


        //checkpoints saves
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            CheckpointsOBJ.GetComponent<CheckpointController>().SetCheckpoint(CheckpointsOBJ.GetComponent<CheckpointController>().Checkpoints[PlayerPrefs.GetInt("Checkpoint")]);
            CheckpointsOBJ.GetComponent<CheckpointController>().LoadCheckpoint();
        }
    }

    public void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SaveSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SaveMusicMute(bool value)
    {
        if(value)
            PlayerPrefs.SetInt("MusicMute", 1);
        else
            PlayerPrefs.SetInt("MusicMute", -1);
    }

    public void SaveSFXMute(bool value)
    {
        if (value)
            PlayerPrefs.SetInt("SFXMute", 1);
        else
            PlayerPrefs.SetInt("SFXMute", -1);
    }

    public void SaveXMouseSens(float value)
    {
        PlayerPrefs.SetFloat("XMouseSensitivity", value);
    }
    public void SaveYMouseSens(float value)
    {
        PlayerPrefs.SetFloat("YMouseSensitivity", value);
    }
    public void SaveXControllerSens(float value)
    {
        PlayerPrefs.SetFloat("XControllerSensitivity", value);
    }
    public void SaveYControllerSens(float value)
    {
        PlayerPrefs.SetFloat("YControllerSensitivity", value);
    }

    public void SaveInvertLook(bool value)
    {
        if (value)
            PlayerPrefs.SetInt("InvertLook", 1);
        else
            PlayerPrefs.SetInt("InvertLook", -1);
    }

    public void SaveCheckpoint(int value)
    {
        PlayerPrefs.SetInt("Checkpoint", value);
    }
}
