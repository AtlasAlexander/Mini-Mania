using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Material offMat, onMat;

    public int RequiredWeight = 50;
    [SerializeField] List<GameObject> DoorsToOpen;
    [SerializeField] List<GameObject> LasersToTrigger;
    [SerializeField] bool TriggerLasersOn = false;
    bool doorOpen = false;
    int ObjOnSwitch = 0;
    private bool buttonClicked = false;

    public GameObject[] circuitBoard;

    public GameObject objOnButton;

    private float noiseTimer;

    Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        noiseTimer += Time.deltaTime;
        if (ObjOnSwitch > 0 && objOnButton.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
        {
            Anim.SetFloat("Pressed", 1f);
            if (circuitBoard != null)
            {
                foreach (var item in circuitBoard)
                {
                    item.GetComponent<MeshRenderer>().material = onMat;
                }
            }

            if (buttonClicked == false)
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("buttonClick", gameObject);
                if(noiseTimer > 0.4f)
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("openDoor", DoorsToOpen[0]);
                    noiseTimer = 0;
                }
                
                buttonClicked = true;
            }
            
            if (DoorsToOpen.Count > 0)
            {
                foreach (GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().OpenDoor(); }
            }
            

            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) { laser.SetActive(TriggerLasersOn); }
            }
        }           
        else
        {
            Anim.SetFloat("Pressed", 0);
            if(circuitBoard != null)
            {
                foreach (var item in circuitBoard)
                {
                    item.GetComponent<MeshRenderer>().material = offMat;
                }
            }

            if (DoorsToOpen.Count > 0)
                {
                    buttonClicked = false;
                    foreach (GameObject door in DoorsToOpen) { door.GetComponent<DoorController>().CloseDoor(); }
                }
            

            if (LasersToTrigger.Count > 0)
            {
                buttonClicked = false;
                foreach (GameObject laser in LasersToTrigger) { 
                    laser.SetActive(!TriggerLasersOn);
                    
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() != null)
        {

            if (other.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
            {
                if(noiseTimer> 0.4f)
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("closeDoor", DoorsToOpen[0]);
                    noiseTimer = 0;
                }
                
            }

            if (ObjOnSwitch > 0)
            {
                ObjOnSwitch--;
            }
        }
        if (other.gameObject.CompareTag("PlayerTrigger"))
        {
            if (noiseTimer > 0.4f)
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("closeDoor", DoorsToOpen[0]);
                noiseTimer = 0;
            }

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
