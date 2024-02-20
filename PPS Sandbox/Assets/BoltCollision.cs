using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCollision : MonoBehaviour
{
    GameObject CheckpointControllerRef;
    // Start is called before the first frame update
    void Start()
    {
        CheckpointControllerRef = GameObject.Find("CheckpointController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            if(CheckpointControllerRef != null)
                CheckpointControllerRef.GetComponent<CheckpointController>().LoadCheckpoint();
            print("Colliding");
        }
    }
}
