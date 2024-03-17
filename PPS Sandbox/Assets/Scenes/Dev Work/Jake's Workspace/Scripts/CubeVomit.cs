using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVomit : MonoBehaviour
{
    public GameObject activeCube, originalCube, assignedRoom;

    private void Awake()
    {
        activeCube.GetComponent<DestroyCube>().AssignedRoom = assignedRoom;
        activeCube.GetComponent<DestroyCube>().Dispenser = gameObject;
    }
    private void FixedUpdate()
    {
        if (activeCube == null)
        {
            activeCube = Instantiate(originalCube, transform.position, transform.rotation);
        }
    }
}
