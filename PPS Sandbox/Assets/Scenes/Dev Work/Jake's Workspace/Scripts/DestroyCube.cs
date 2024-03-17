using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject Dispenser;
    public GameObject AssignedRoom;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goop")
        {
            transform.position = Dispenser.transform.position;
            transform.rotation = Dispenser.transform.rotation;
            if(Dispenser.GetComponent<CubeVomit>().originalCube.GetComponent<SizeChange>().startSmall)
            {
                transform.localScale = new Vector3(.4f, .4f, .4f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == AssignedRoom.GetComponent<Collider>())
        {
            transform.rotation = Dispenser.transform.rotation;
            transform.position = Dispenser.transform.position;
            if (Dispenser.GetComponent<CubeVomit>().originalCube.GetComponent<SizeChange>().startSmall)
            {
                transform.localScale = new Vector3(.4f, .4f, .4f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }
}
