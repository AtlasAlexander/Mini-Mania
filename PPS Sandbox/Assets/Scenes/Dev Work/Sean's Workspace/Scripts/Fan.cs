using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanPowerForPlayer;
    [SerializeField] private float fanPowerForObjects;
    private GameObject player;
    private float playerGravityValue;
    private Vector3 goalPos;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerGravityValue = player.GetComponent<FirstPersonController>().GetGravity();
        goalPos = this.gameObject.transform.GetChild(0).position;
    }

    private void OnTriggerEnter(Collider other)
    {
 
    }

    private void OnTriggerStay(Collider other)
    {
        //Check if player is colliding
        if (other.GetComponent<CharacterController>() != null)
        {
            //Is Player Shrunk
            if(other.GetComponent<SizeChange>().GetShrunkStatus())
            {
                other.GetComponent<FirstPersonController>().SetMoveDirY(0);
                other.GetComponent<FirstPersonController>().SetGravity(0f);

                float direction = (goalPos.y - other.transform.position.y);

                other.GetComponent<CharacterController>().Move(new Vector3(0, direction * Time.deltaTime * fanPowerForPlayer, 0));
            }
            else
            {
                other.GetComponent<FirstPersonController>().SetGravity(playerGravityValue);
            }
        }

        //Does Object have RigidBody
        if(other.gameObject.GetComponent<Rigidbody>() != null)
        {
            //If Object cannot change size
            if(other.GetComponent<SizeChange>() == null)
            {
                //other.GetComponent<Rigidbody>().velocity = (Vector2.up * fanPowerForObjects * Time.deltaTime);
            }
            //If Object can change size
            else
            {
                //If object is shrunk and not in hands
                if(other.GetComponent<SizeChange>().GetShrunkStatus() && !other.GetComponent<PickUpForObj>().InHand)
                {
                    float direction = (goalPos.y - other.transform.position.y);

                    other.GetComponent<Rigidbody>().velocity = new Vector3(0, direction * Time.deltaTime * fanPowerForObjects, 0);
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

        other.GetComponent<FirstPersonController>().SetGravity(playerGravityValue);
    }

}
