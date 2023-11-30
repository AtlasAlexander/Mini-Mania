using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    GameObject player;
    [SerializeField] List<GameObject> Checkpoints;
    GameObject currentCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentCheckpoint = Checkpoints[0];
    }
    public void SetCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public GameObject GetCheckpoint()
    {
        return currentCheckpoint;
    }
     
    public void LoadCheckpoint()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
