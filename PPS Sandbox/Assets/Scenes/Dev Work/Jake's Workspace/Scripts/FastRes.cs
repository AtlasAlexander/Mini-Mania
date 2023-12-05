using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRes : MonoBehaviour
{
    public GameObject[] resPoint;
    public int i;
    public bool test;
    public GameObject player;

    private void Awake()
    {
        player = gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SizeOverride")
        {
            i = other.GetComponent<FastResSelect>().respoint;
        }
        if(other.tag == "Goop")
        {
            player.transform.position = resPoint[i].transform.position;
            player.GetComponent<CharacterController>().enabled = false;
            test = true;
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
