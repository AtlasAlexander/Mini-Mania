using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionSound : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        // Play sound when the button is selected
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("navigateMenu", FindObjectOfType<Camera>().gameObject);
    }
}
