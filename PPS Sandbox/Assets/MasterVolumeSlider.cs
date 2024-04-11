using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    void Update()
    {
        FindObjectOfType<FmodAudioManager>().masterVolume = GetComponentInChildren<Slider>().value;
    }
}


