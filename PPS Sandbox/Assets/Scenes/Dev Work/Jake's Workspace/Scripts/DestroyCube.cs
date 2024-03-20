using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject Dispenser;
    public GameObject AssignedRoom;

    bool OoB;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goop")
        {
            Dispenser.GetComponent<CubeVomit>().particleTriggerEvent();
            StartCoroutine(RespawnDelay());
            OoB = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(Dispenser != null)
        {
            if (other == AssignedRoom.GetComponent<Collider>())
            {
                Dispenser.GetComponent<CubeVomit>().particleTriggerEvent();
                StartCoroutine(RespawnDelay());
                OoB = true;
            }
        }
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(0.25f);
        OoB = false;
        transform.rotation = Dispenser.transform.rotation;
        transform.position = Dispenser.transform.position;
        if (Dispenser.GetComponent<CubeVomit>().originalCube.GetComponent<SizeChange>().startSmall)
        {
            transform.localScale = new Vector3(.4f, .4f, .4f);
        }
        else
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    private void Update()
    {
        if(OoB)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.parent = null;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
