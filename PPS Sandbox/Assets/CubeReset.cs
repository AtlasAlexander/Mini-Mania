using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeReset : MonoBehaviour
{
    [Header("VARIABLES")]
    public GameObject[] cubes;
    public Vector3[] cubePos;
    int i = 0;

    private void Start()
    {
        foreach(GameObject cube in cubes)
        {
            print(cube.transform.position);
            cubePos[i] = cube.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.transform.position = cubePos[i];
        }
    }
}
