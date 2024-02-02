using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanPowerForPlayer;
    [SerializeField] private float fanPowerForObjects;
    [SerializeField] private GameObject player;
    private float playerGravityValue;
    private Vector3 goalPos;

    private void Start()
    {
        playerGravityValue = player.GetComponent<FirstPersonController>().GetGravity();
        goalPos = this.gameObject.transform.GetChild(0).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If other is not player, return
        if (!other.GetComponent<FirstPersonController>())
        {
            return;
        }

        //If player is not shrunk, return
        if (!other.GetComponent<SizeChange>().GetShrunkStatus())
        {
            return;
        }

        other.GetComponent<FirstPersonController>().SetGravity(0f);
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if player is colliding
        if (other.GetComponent<CharacterController>() != null)
        {
            //Is Player Shrunk
            if(other.GetComponent<SizeChange>().GetShrunkStatus())
            {
                /* Vector3 direction = (goalPos - other.transform.position);

                 other.GetComponent<CharacterController>().Move(new Vector3(0, direction.y, 0));*/

                float direction = (goalPos.y - other.transform.position.y);

                other.GetComponent<CharacterController>().Move(new Vector3(0, direction * Time.deltaTime * fanPowerForPlayer, 0));
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

        other.GetComponent<FirstPersonController>().SetGravity(playerGravityValue);
    }

}
