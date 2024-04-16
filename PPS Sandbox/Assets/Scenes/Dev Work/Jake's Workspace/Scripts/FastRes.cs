using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRes : MonoBehaviour
{
    public GameObject[] resPoint;
    public int i;
    public bool test;
    public GameObject player;
    GameObject CheckpointRef;

    private void Awake()
    {
        player = gameObject;
        CheckpointRef = GameObject.Find("CheckpointController");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SizeOverride")
        {
            i = other.GetComponent<FastResSelect>().respoint;
        }
        if(other.tag == "Goop")
        {
            //player.transform.position = resPoint[i].transform.position;
            Debug.Log("Death by goop");
            player.GetComponent<CharacterController>().enabled = false;
            test = true;
            CheckpointRef.GetComponent<CheckpointController>().checkChange = true;
            CheckpointRef.GetComponent<CheckpointController>().LoadCheckpoint();
        }
        
    }
    private void Update()
    {
        if(test)
        {
            player.GetComponent<CharacterController>().enabled = true;
            test = false;
        }
    }
}
