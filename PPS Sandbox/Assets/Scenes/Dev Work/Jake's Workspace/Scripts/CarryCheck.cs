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
        if (UserInput.instance.InteractInput)
        {
            carrying = !carrying;
        }
        {
            ProcessRaycast();
        }
    }


    private void ProcessRaycast()
    {
        Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward * pickUpRange, Color.green);
        //Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward * (pickUpRange - 1.0f), Color.red);
        //var carryObject = carry.gameObject.GetComponent<Stats>().Weight
        ///checks if object is in range and centred on camera
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, pickUpRange))
        {
            //Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward, Color.green);
            CarryObj carry = hit.transform.GetComponent<CarryObj>();
            //Debug.Log($"The Ray has hit at {hit}");
            /*var carryObject = carry.gameObject.GetComponent<Stats>().Weight;
            var playerWeight = Player.GetComponent<Stats>().Weight;
            Debug.Log($"This is from CarryObj class: {carryObject}");
            Debug.Log($"This is from the weight of player: {playerWeight}");*/

            if (carry == null)
            {
                //carrying = false;
                return;
            }
            if (carry != null)
            {
                //if target is in range
                Debug.Log("HIT");
                if (carrying)
                {
                    if (carry.gameObject.GetComponent<Stats>().Weight == Player.GetComponent<Stats>().Weight)
                    {
                        carry.isHoldingLargeObject = true;
                        carry.isHoldingSmallObject = false;
                    }
                    else if (carry.gameObject.GetComponent<Stats>().Weight < Player.GetComponent<Stats>().Weight)
                    {
                        carry.isHoldingSmallObject = true;
                        carry.isHoldingLargeObject = false;
                    }
                    else
                    {
                        carrying = false; ;
                    }
                }

                if(!carrying)
                {
                    carry.isHoldingLargeObject = false;
                    carry.isHoldingSmallObject = false;
                }
            }
            else
            {
                //carrying = false;
                return;
            }

        }
        else
        {
            return;
        }
    }
}
