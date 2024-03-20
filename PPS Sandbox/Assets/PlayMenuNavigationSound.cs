using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMenuNavigationSound : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        // Play sound when the button is selected
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", GameObject.Find("MenuListener").gameObject);

    }
}
