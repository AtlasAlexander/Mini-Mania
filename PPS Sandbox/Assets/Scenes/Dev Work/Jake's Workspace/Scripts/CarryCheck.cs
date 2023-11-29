using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryCheck : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float pickUpRange = 3f;

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
        playerControls.Movement.Interact.performed += x => IsCarrying();
        {
            ProcessRaycast();
        }
    }

    void IsCarrying()
    {
        carrying = !carrying;
    }

    private void ProcessRaycast()
    {

        ///checks if object is in range and centred on camera
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, pickUpRange))
        {
            
            CarryObj carry = hit.transform.GetComponent<CarryObj>();

            if (carry == null)
            {
                carrying = false;
                return;
            }
            if (carry != null)
            {
                //if target is in range
                Debug.Log("HIT");
                if (carrying)
                {
                    if (carry.gameObject.GetComponent<Stats>().Weight < Player.GetComponent<Stats>().Weight)
                    {
                        carry.holding = true;
                    }
                    else
                    {
                        carrying = false; ;
                    }
                }

                if(!carrying)
                {
                    carry.holding = false;
                }
            }
            else
            {
                carrying = false;
                return;
            }

        }
        else
        {
            return;
        }
    }
}
