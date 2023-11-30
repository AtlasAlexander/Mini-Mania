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
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (ObjOnSwitch > 0)
        {
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
                foreach (GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().CloseDoor(); }
            }
            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(!TriggerLasersOn); }
            }
        }           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() != null)
        {
            if (other.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
            {
                ObjOnSwitch++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() != null)
        {
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
                if (other.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
                {
                    ObjOnSwitch++;
                }
            }
        }
    }
}
