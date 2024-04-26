using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject Dispenser;
    public GameObject AssignedRoom;

    public GameObject player;

    bool OoB, startCubeStasis;
    float timer;

    public bool newCube;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goop")
        {
            Dispenser.GetComponent<CubeVomit>().particleTriggerEvent();
            StartCoroutine(RespawnDelay());
            OoB = true;
            if (newCube)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(Dispenser != null)
        {
            if (other == AssignedRoom.GetComponent<Collider>())
            {
                Dispenser.GetComponent<CubeVomit>().particleTriggerEvent();
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                StartCoroutine(RespawnDelay());
                OoB = true;
            }
        }
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(0.45f);
        OoB = false;
        startCubeStasis = true;
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
            player.GetComponent<NewGrabbing>().DropObject();
        }
        else
        {
            if (startCubeStasis)
            {
                KeepingTheCubeThere();
            }
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void KeepingTheCubeThere()
    {
        GetComponent<MeshRenderer>().enabled = true;
        timer += Time.deltaTime;
        if (timer <= 1.0f)
        {
            transform.rotation = Dispenser.transform.rotation;
            transform.position = Dispenser.transform.position;
        }
        else
        {
            timer = 0;
            startCubeStasis = false;
        }
        
    }
}
