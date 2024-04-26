using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicVolumeSlider : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
    }
    void Update()
    {
        FindObjectOfType<FmodAudioManager>().musicVolume = GetComponentInChildren<Slider>().value;
    }
}
