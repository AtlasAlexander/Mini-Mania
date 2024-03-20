using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMenuSoundOnButtonPress : MonoBehaviour, ISelectHandler
{
    public string soundName;
    public void OnSelect(BaseEventData eventData)
    {
        if(soundName!= null)
        {
            print("Sound ot found");
            // Play sound when the button is selected
            FindObjectOfType<FmodAudioManager>().QuickPlaySound(soundName, GameObject.Find("MenuListener").gameObject);
        }
        else
        {
            print("Sound " + soundName + " is not found");
        }
        print("Sound ot found");
    }
}
