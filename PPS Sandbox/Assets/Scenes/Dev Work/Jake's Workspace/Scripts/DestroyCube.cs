using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject Dispenser;
    public GameObject AssignedRoom;
    public FastRes fastRes;

    public GameObject player;

    bool OoB, startCubeStasis;
    bool respawned;
    float timer;

    public bool newCube;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fastRes = gameObject.GetComponent<FastRes>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goop")
        {
            Respawn();
            NewRes();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(Dispenser != null)
        {
            if (other == AssignedRoom.GetComponent<Collider>())
            {
                NewRes();
            }
        }
    }

    public void NewRes()
    {
        Dispenser.GetComponent<CubeVomit>().particleTriggerEvent();
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<Rigidbody>().useGravity = false;
        //StartCoroutine(RespawnDelay());
        transform.rotation = Dispenser.transform.rotation;
        transform.position = Dispenser.transform.position;
        transform.parent = null;
        player.GetComponent<NewGrabbing>().DropObject();
    }

    public void Respawn()
    {
        if (newCube)
        {
            Destroy(gameObject);
        }
    }
}
