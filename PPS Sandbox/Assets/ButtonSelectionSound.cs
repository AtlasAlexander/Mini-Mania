using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionSound : MonoBehaviour, ISelectHandler
{
    private float spamTimer = 0.0f;



    public void OnSelect(BaseEventData eventData)
    {
        // Play sound when the button is selected
        if(spamTimer > 0.7f)
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("navigateMenu", GameObject.FindGameObjectWithTag("MainCamera"));
            spamTimer = 0;
        }
        
    }

    private void Update()
    {
        
        spamTimer = spamTimer + Time.deltaTime;
    }
}
