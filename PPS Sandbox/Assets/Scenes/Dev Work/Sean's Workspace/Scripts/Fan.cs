using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanPowerForPlayer;
    [SerializeField] private float fanPowerForObjects;
    [SerializeField] private GameObject player;
    private float playerGravityValue;

    private void Start()
    {
        playerGravityValue = player.GetComponent<FirstPersonController>().GetGravityValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If other is not player, return
        if (other.GetComponent<CharacterController>() == null)
        {
            return;
        }

        //If player is not shrunk, return
        if (!other.GetComponent<SizeChange>().GetShrunkStatus())
        {
            return;
        }

        other.GetComponent<PlayerController>().SetGravityValue(0);
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if player is colliding
        if (other.GetComponent<CharacterController>() != null)
        {
            //Is Player Shrunk
            if(other.GetComponent<SizeChange>().GetShrunkStatus())
            {
                other.GetComponent<CharacterController>().Move(Vector2.up * fanPowerForPlayer * Time.deltaTime);
            }
        }

        //Does Object have RigidBody
        if(other.gameObject.GetComponent<Rigidbody>() != null)
        {
            //If Object cannot change size
            if(other.GetComponent<SizeChange>() == null)
            {
                other.GetComponent<Rigidbody>().velocity = (Vector2.up * fanPowerForObjects * Time.deltaTime);
            }
            //If Object can change size
            else
            {
                //If object is shrunk
                if(other.GetComponent<SizeChange>().GetShrunkStatus())
                {
                    other.GetComponent<Rigidbody>().velocity = (Vector2.up * fanPowerForObjects * Time.deltaTime);
                }
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If other is not player, return
        if (other.GetComponent<CharacterController>() == null)
        {
            return;
        }


        //If player is not shrunk, return
        if (!other.GetComponent<SizeChange>().GetShrunkStatus())
        {
            return;
        }

        other.GetComponent<PlayerController>().SetGravityValue(playerGravityValue);
    }
}
