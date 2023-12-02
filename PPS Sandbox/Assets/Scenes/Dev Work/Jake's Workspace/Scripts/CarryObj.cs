using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObj : MonoBehaviour
{
    public GameObject smallObject;
    public GameObject largeObject;
    public bool isHoldingLargeObject;
    public bool isHoldingSmallObject;
    private float speed;
    public float outOfLosDist = 4f;

    private void Awake()
    {
        smallObject = GameObject.FindGameObjectWithTag("SmallObject");
        largeObject = GameObject.FindGameObjectWithTag("LargeObject");
    }

    private void Update()
    {
        if (isHoldingSmallObject)
        {
            PickUpSmallObject();
        }
        else if(isHoldingLargeObject)
        {
            PickUpBigObject();
        }
        else
        {
            Dropped();
        }
        if(!smallObject.GetComponent<CarryCheck>().carrying)
        {
            isHoldingSmallObject = false;
            isHoldingLargeObject = false;
        }
    }
    public void PickUpSmallObject()
    {
        ///adds flow to carrying objects
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, smallObject.transform.position, step);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.rotation = Quaternion.identity;

        ///if object held is out of "arms reach"
        if (Vector3.Distance(transform.position, smallObject.transform.position) > outOfLosDist)
        {
            isHoldingLargeObject = false;
        }

        //transform.position = theseHands.transform.position;
        //transform.rotation = theseHands.transform.rotation;

    }

    public void PickUpBigObject()
    {
        ///adds flow to carrying objects
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, largeObject.transform.position, step);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.rotation = Quaternion.identity;

        ///if object held is out of "arms reach"
        if (Vector3.Distance(transform.position, largeObject.transform.position) > outOfLosDist)
        {
            isHoldingLargeObject = false;
        }

        //transform.position = theseHands.transform.position;
        //transform.rotation = theseHands.transform.rotation;

    }

    public void Dropped()
    {
        transform.parent = null;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        ///stops phasing through objects if "hands" are too far away
        speed = 2f;
    }

    private void OnCollisionExit(Collision collision)
    {
        speed = 20f;
    }
}
