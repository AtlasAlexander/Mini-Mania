using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewGrabbing : MonoBehaviour
{
    [Header("PICKUP")]
    public Transform holdArea;
    public GameObject heldObj;
    private Rigidbody heldObjRb;
    public bool grab;
    InputAction grabInput;

    bool startTimer = false;
    float time;


    [Header("PHYSICS")]
    public float pickUpRange = 5.0f;
    public float pickUpForce = 150f;

    [Header("REFERENCES")]
    public Camera cam;
    SizeChange sizeChange;
    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        grabInput = playerControls.Movement.Interact;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (grabInput.WasPressedThisFrame())
        {
            if (heldObj == null)
            {
                RaycastHit hitData;
                Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * pickUpRange, Color.red);
                if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitData, pickUpRange))
                {
                    if (hitData.transform.gameObject.GetComponent<SizeChange>())
                    {
                        sizeChange = hitData.transform.gameObject.GetComponent<SizeChange>();
                        if (!sizeChange.isChangingSize && sizeChange.Pickupable())
                        {
                            PickUpObject(hitData.transform.gameObject);
                        }
                    }
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

        if (grab)
        {
            time += Time.deltaTime;
        }

        if (heldObj != null)
        {
            float dis = Vector3.Distance(heldObj.transform.position, holdArea.transform.position);
            print(dis);
            if (dis >= 1f && time > 1f)
            {
                DropObject();
            }
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
        time = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            DropObject();
        }
    }
}
