using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicVolumeSlider : MonoBehaviour
{
    void Update()
    {
        FindObjectOfType<FmodAudioManager>().musicVolume = GetComponentInChildren<Slider>().value;
    }
}
