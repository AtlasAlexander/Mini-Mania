using FMOD.Studio;
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
    private GameObject hands;

    public EventInstance fanSound;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            playerGravityValue = player.GetComponent<FirstPersonController>().GetGravity();
        }
        goalPos = this.gameObject.transform.GetChild(0).position;
        hands = GameObject.Find("Hands");

        fanSound = FMODUnity.RuntimeManager.CreateInstance(FindObjectOfType<FmodAudioManager>().gameplaySounds[FindObjectOfType<FmodAudioManager>().FindEventReferenceByName("fanBuzz")]);
        fanSound.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fanSound, gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            //Is Player Shrunk
            if (other.GetComponent<SizeChange>().GetShrunkStatus())
            {
                other.GetComponent<FirstPersonController>().SetMoveDirY(0);
                other.GetComponent<FirstPersonController>().SetGravity(0f);

                float direction = (goalPos.y - other.transform.position.y);
                
                if(direction > 2f)
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("fanBoost", player);
                   
                }
                else 
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("airWhoosh", player);
                  
                }
                
            }
        }
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
               
                other.GetComponent<FirstPersonController>().inFan = true;
                other.GetComponent<FirstPersonController>().SetGravity(playerGravityValue);
            }

        }
       


        if (other.gameObject.GetComponent<Rigidbody>() == null)
        {
            return;
        }
        if (other.GetComponent<SizeChange>() == null)
        {
            return;
        }
        if(other.gameObject.transform.parent == hands.transform)
        {
            return;
        }

        //If object is shrunk and not in hands
        if (other.GetComponent<SizeChange>().GetShrunkStatus())
        {
            float direction = (goalPos.y - other.transform.position.y);

            other.GetComponent<Rigidbody>().velocity = new Vector3(0, direction * Time.deltaTime * fanPowerForObjects, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If other is not player, return
        if (other.GetComponent<CharacterController>() == null)
        {
            return;
        }else
        {
            player.GetComponent<FirstPersonController>().inFan = false;
        }


        other.GetComponent<FirstPersonController>().SetGravity(playerGravityValue);
    }

}