using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRes : MonoBehaviour
{
    public GameObject resPoint;
    public bool test;
    public GameObject player;

    private void Awake()
    {
        player = gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goop")
        {
            Instantiate(player, resPoint.transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(test)
        {
            Destroy(gameObject);
        }
    }
}
