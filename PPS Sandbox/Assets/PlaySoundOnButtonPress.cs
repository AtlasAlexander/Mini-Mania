using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnButtonPress : MonoBehaviour, ISelectHandler
{
    public string soundName;
    public void OnSelect(BaseEventData eventData)
    {
        if(soundName!= null)
        {
            // Play sound when the button is selected
            FindObjectOfType<FmodAudioManager>().QuickPlaySound(soundName, FindObjectOfType<Camera>().gameObject);
        }
    }
}
