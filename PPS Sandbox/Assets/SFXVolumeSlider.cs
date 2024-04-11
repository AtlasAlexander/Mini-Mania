using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SFXVolumeSlider : MonoBehaviour
{
    void Update()
    {
        FindObjectOfType<FmodAudioManager>().soundEffectsVolume = GetComponentInChildren<Slider>().value;
    }
}
