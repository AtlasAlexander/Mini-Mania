using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionSound : MonoBehaviour, ISelectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("navigateMenu", FindObjectOfType<Camera>().gameObject);

    }
}