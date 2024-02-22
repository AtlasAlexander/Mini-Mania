using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrabbing : MonoBehaviour
{
    [Header("PICKUP")]
    public Transform holdArea;
    public GameObject heldObj;
    private Rigidbody heldObjRb;
    public bool grab;

    [Header("PHYSICS")]
    public float pickUpRange = 5.0f;
    public float pickUpForce = 150f;

    private void Update()
    {
        if (Input.GetButtonDown("Grab"))
        {
            if (heldObj == null)
            {
                RaycastHit hitData;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitData, pickUpRange))
                {
                    PickUpObject(hitData.transform.gameObject);
                }
            }

            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
        }
              
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDir = (holdArea.position - heldObj.transform.position);
            heldObjRb.AddForce(moveDir * pickUpForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.drag = 10;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRb.transform.parent = holdArea;
            heldObj = pickObj;
            grab = true;
        }
    }

    void DropObject()
    {
        heldObjRb.useGravity = true;
        heldObjRb.drag = 1;
        heldObjRb.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;
        grab = false;

    }
}
