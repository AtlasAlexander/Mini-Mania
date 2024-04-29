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
    public List<GameObject> cubes = new List<GameObject>();
    public NewGrabbing grabbing;

    private void Awake()
    {
        player = gameObject;
        CheckpointRef = GameObject.Find("CheckpointController");
        grabbing = player.GetComponent<NewGrabbing>();
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

            if (grabbing.heldObj !=null)
            {
                grabbing.heldObj.GetComponent<DestroyCube>().Respawn();
            }

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
