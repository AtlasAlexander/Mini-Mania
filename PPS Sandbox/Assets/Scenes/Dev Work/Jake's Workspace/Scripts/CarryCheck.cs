using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryCheck : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 3f;
    [SerializeField] InputAction pickUp;

    GameObject Player;

    private void Awake()
    {
        FPCamera = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        //if (pickUp.ReadValue<bool>())
        {
            ProcessRaycast();
        }
    }
    private void ProcessRaycast()
    {

        ///checks if object is in range and centred on camera
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            
            CarryObj carry = hit.transform.GetComponent<CarryObj>();

            if (carry == null)
            {
                return;
            }
            if (carry != null)
            {
                //if target is in range
                Debug.Log("HIT");
                if (Input.GetButton("Grab"))
                {
                    if (carry.gameObject.GetComponent<Stats>().Weight < Player.GetComponent<Stats>().Weight)
                    {
                        carry.holding = true;
                    }
                }

                if (Input.GetButtonUp("Grab"))
                {
                    carry.holding = false;
                }
            }
            else
            {
                return;
            }

        }
        else
        {
            return;
        }
    }
}
