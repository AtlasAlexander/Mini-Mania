using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public int RequiredWeight = 50;
    [SerializeField] GameObject DoorToOpen;
    bool doorOpen = false;
    int ObjOnSwitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 3  && !doorOpen)
        {
            doorOpen = true;
            ObjOnSwitch++;
            DoorToOpen.GetComponent<DoorController>().OpenDoor();
        }
        else if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 3 && doorOpen)
        {
            ObjOnSwitch--;
            if (ObjOnSwitch == 0)
            {
                doorOpen = false;
                DoorToOpen.GetComponent<DoorController>().CloseDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>().Weight > 50)
        {
            ObjOnSwitch++;
            DoorToOpen.GetComponent<DoorController>().OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ObjOnSwitch == 0)
        {
            DoorToOpen.GetComponent<DoorController>().CloseDoor();
        }
    }
}
