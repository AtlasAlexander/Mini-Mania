using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public int RequiredWeight = 50;
    [SerializeField] List<GameObject> DoorsToOpen;
    [SerializeField] List<GameObject> LasersToTrigger;
    [SerializeField] bool TriggerLasersOn = false;
    bool doorOpen = false;
    int ObjOnSwitch = 0;
    private bool buttonClicked = false;

    public GameObject objOnButton;
    void Update()
    {
        if (ObjOnSwitch > 0 && objOnButton.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
        {
            if(buttonClicked == false)
            {
                //FindObjectOfType<FmodAudioManager>().QuickPlaySound("buttonClick", gameObject);
                //FindObjectOfType<FmodAudioManager>().QuickPlaySound("openDoor", DoorsToOpen[0]);
                buttonClicked = true;
            }
            if (DoorsToOpen.Count > 0)
            {
                foreach(GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().OpenDoor(); }
            }
            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(TriggerLasersOn); }
            }
        }           
        else
        {
            if (DoorsToOpen.Count > 0)
            {
                buttonClicked = false;
                foreach (GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().CloseDoor(); }
            }
            if (LasersToTrigger.Count > 0)
            {
                buttonClicked = false;
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(!TriggerLasersOn); }
            }
        }           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() != null)
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("closeDoor", DoorsToOpen[0]);
            if (ObjOnSwitch > 0)
            {
                ObjOnSwitch--;
            }
        }
        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("closeDoor", DoorsToOpen[0]);
            if (ObjOnSwitch > 0)
            {
                ObjOnSwitch--;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ObjOnSwitch == 0)
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                objOnButton = other.gameObject;
                if (other.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
                {
                    ObjOnSwitch++;
                }
            }
            if (other.gameObject.CompareTag("PlayerTrigger"))
            {
                objOnButton = other.transform.parent.gameObject;
                if (other.gameObject.GetComponentInParent<Stats>().Weight > RequiredWeight)
                {
                    ObjOnSwitch++;
                }
            }
        }
    }
}
