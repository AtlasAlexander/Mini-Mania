using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObj : MonoBehaviour
{
    public GameObject theseHands;
    public bool holding;
    private float speed;

    private void Awake()
    {
        theseHands = GameObject.FindGameObjectWithTag("Hands");
    }

    private void Update()
    {
        if (holding)
        {
            PickedUp();
        }
        else
        {
            Dropped();
        }
    }
    public void PickedUp()
    {
        ///adds flow to carrying objects
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, theseHands.transform.position, step);
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        ///if object held is out of "arms reach"
        if (Vector3.Distance(transform.position, theseHands.transform.position) > 4f)
        {
            holding = false;
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
