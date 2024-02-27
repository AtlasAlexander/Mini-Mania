using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedByGoop : RespawnPoints
{
    //
    //[SerializeField] private GameObject playerTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Destroyed!");
            SetPlayerSpawnpoint();
        }

        if (other.CompareTag("Pickup"))
        {
            Debug.Log("Cube Destroyed!");
            SetCubePosition();
        }
    }
}
