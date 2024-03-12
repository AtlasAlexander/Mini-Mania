using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpForObj : MonoBehaviour
{
    public float followSpeed;
    private float defaultSpeed;

    public bool InHand;

    public GameObject hands;

    private void Awake()
    {
        hands = GameObject.FindGameObjectWithTag("Hands");
        defaultSpeed = followSpeed;
    }
    private void Update()
    {
        if (InHand)
        {
            ObjInHand();
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void ObjInHand()
    {
        if(!hands.GetComponent<PickUpForHands>().carrying)
        {
            InHand = false;
        }
        //adds flow to carrying objects
        var step = defaultSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, hands.transform.position, step);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.rotation = Quaternion.identity;

        //if object held is out of "arms reach"
        if (Vector3.Distance(transform.position, hands.transform.position) > hands.GetComponent<PickUpForHands>().outOfLosDist)
        {
            hands.GetComponent<PickUpForHands>().carrying = false;
            InHand = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //stops phasing through objects if "hands" are too far away
        followSpeed = defaultSpeed/100;
    }

    private void OnCollisionExit(Collision collision)
    {
        followSpeed = defaultSpeed;
    }
}
