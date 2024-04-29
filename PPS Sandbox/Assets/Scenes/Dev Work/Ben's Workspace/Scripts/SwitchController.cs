using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Material offMat, onMat;
    Animator anim;
    public Material ButtonOnMatt, ButtonOffMat;

    public int RequiredWeight = 50;
    [SerializeField] List<GameObject> DoorsToOpen;
    [SerializeField] List<GameObject> LasersToTrigger;
    [SerializeField] bool TriggerLasersOn = false;
    [SerializeField] bool invertedSound = false;
    [SerializeField] int laserVolume = 10;
    [SerializeField] bool soundOverride = false;
    bool doorOpen = false;
    int ObjOnSwitch = 0;
    private bool buttonClicked = false;

    public GameObject[] circuitBoard;
    public GameObject mirroredButton;

    public GameObject objOnButton;

    private float noiseTimer;


    private void Start()
    {

        anim = GetComponent<Animator>();
        GetComponentInChildren<MeshRenderer>().material = ButtonOffMat;
    }
    void Update()
    {
        noiseTimer += Time.deltaTime;
        if (ObjOnSwitch > 0 && objOnButton.gameObject.GetComponent<Stats>().Weight > RequiredWeight)
        {
            anim.SetFloat("Pressed", 1f);
            GetComponentInChildren<MeshRenderer>().material = ButtonOnMatt;
            if (circuitBoard != null)
            {
                foreach (var item in circuitBoard)
                {
                    item.GetComponent<MeshRenderer>().material = onMat;
                }
            }
            if(mirroredButton != null)
            {
                mirroredButton.GetComponentInChildren<MeshRenderer>().material = ButtonOnMatt;
            }

            if (buttonClicked == false)
            {
                if (!soundOverride)
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("buttonClick", gameObject);
                }
                
                if(noiseTimer > 0.4f)
                {
                    if(DoorsToOpen.Count > 0)
                        FindObjectOfType<FmodAudioManager>().QuickPlaySound("openDoor", DoorsToOpen[0]);
                    noiseTimer = 0;
                }
                
                buttonClicked = true;
            }
            
            if (DoorsToOpen.Count > 0)
            {
                foreach (GameObject door in DoorsToOpen) if (door != null) { { door.GetComponent<DoorController>().OpenDoor(); } }
            }
            

            if (LasersToTrigger.Count > 0)
            {
                foreach (GameObject laser in LasersToTrigger) if (laser != null) { { 
                            laser.SetActive(TriggerLasersOn);
                            if (invertedSound)
                            {
                                laser.GetComponent<laser>().SetLaserVolume(laserVolume);
                            }
                            else
                            {
                                laser.GetComponent<laser>().SetLaserVolume(0);
                            }
                        } }
            }
        }           
        else
        {
            anim.SetFloat("Pressed", 0f);
            GetComponentInChildren<MeshRenderer>().material = ButtonOffMat;
            if (mirroredButton != null)
            {
                mirroredButton.GetComponentInChildren<MeshRenderer>().material = ButtonOffMat;
            }

            if (circuitBoard != null)
            {
                foreach (var item in circuitBoard)
                {
                    item.GetComponent<MeshRenderer>().material = offMat;
                }
            }

            if (DoorsToOpen.Count > 0)
                {
                    buttonClicked = false;
                foreach (GameObject door in DoorsToOpen) if (door != null) { { door.GetComponent<DoorController>().CloseDoor(); } }
                }
            

            if (LasersToTrigger.Count > 0)
            {
                buttonClicked = false;
                foreach (GameObject laser in LasersToTrigger) if (laser != null) { { 
                            laser.SetActive(!TriggerLasersOn);
                            if (invertedSound)
                            {
                                laser.GetComponent<laser>().SetLaserVolume(0);
                            }
                            else
                            {
                                laser.GetComponent<laser>().SetLaserVolume(laserVolume);
                            }
                            
                        } }
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
                    if(DoorsToOpen.Count > 0)
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

                else
                {
                    return;
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
