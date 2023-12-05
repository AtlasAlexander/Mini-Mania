using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVomit : MonoBehaviour
{
    public GameObject activeCube, originalCube;
    private void FixedUpdate()
    {
        if (activeCube == null)
        {
            activeCube = Instantiate(originalCube, transform.position, transform.rotation);
        }
    }
}
