using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SFXVolumeSlider : MonoBehaviour
{
    private void Awake()
    {
        if(PlayerPrefs.HasKey("SFXVolume"))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
    }

    void Update()
    {
        FindObjectOfType<FmodAudioManager>().soundEffectsVolume = GetComponentInChildren<Slider>().value;
    }
}
