using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    private void Awake()
    {
/*        if (PlayerPrefs.HasKey("MasterVolume"))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");*/
    }
    void Update()
    {
        FindObjectOfType<FmodAudioManager>().masterVolume = GetComponentInChildren<Slider>().value;
    }
}


