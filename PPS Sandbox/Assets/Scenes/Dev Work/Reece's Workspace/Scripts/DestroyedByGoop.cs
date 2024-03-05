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
            Debug.Log("Respawned Back to Checkpoint");
            SetPlayerSpawnpoint();
        }

        if (other.name == "CarryCube Big")
        {
            Debug.Log("Cube 1 Respawned!");
            SetCube1Position();
        }

        if (other.name == "CarryCube Big (1)")
        {
            Debug.Log("Cube 2 Respawned!");
            SetCube2Position();
        }
    }
}
