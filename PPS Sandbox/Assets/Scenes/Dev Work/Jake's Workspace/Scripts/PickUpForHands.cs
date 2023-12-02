using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpForHands : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float pickUpRange = 3f;
    public float outOfLosDist = 4f;

    GameObject Player;
    public PlayerControls playerControls;

    public bool carrying;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Awake()
    {
        FPCamera = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        //check for input
        playerControls.Movement.Interact.performed += x => IsCarrying();
    }

    void IsCarrying()
    {
        //toggle
        carrying = !carrying;
        //raycast
        if(carrying)
        {
            ProcessRaycast();
        }
    }

    void ProcessRaycast()
    {
        //send raycast
        RaycastHit hit;
        //if there is an object in range
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, pickUpRange))
        {
            //checks for pick up script on obj
            PickUpForObj carry = hit.transform.GetComponent<PickUpForObj>();
            if(carry != null && carry.GetComponent<Stats>().Weight <= Player.GetComponent<Stats>().Weight)
            {
                carry.InHand = true;
            }
            else
            {
                carrying = false;
            }
        }
        //if there is not an object in range
        else
        {
            carrying = false;
        }
    }
}
