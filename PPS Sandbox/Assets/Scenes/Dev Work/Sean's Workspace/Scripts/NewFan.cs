using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        /*//Check if player is colliding
        if (other.GetComponent<CharacterController>() != null)
        {
            //Is Player Shrunk
            if (other.GetComponent<SizeChange>().GetShrunkStatus())
            {
                other.GetComponent<CharacterController>().Move(Vector2.up * fanPowerForPlayer * Time.deltaTime);
            }
        }

        //Does Object have RigidBody
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            //If Object cannot change size
            if (other.GetComponent<SizeChange>() == null)
            {
                other.GetComponent<Rigidbody>().velocity = (Vector2.up * fanPowerForObjects * Time.deltaTime);
            }
            //If Object can change size
            else
            {
                //If object is shrunk
                if (other.GetComponent<SizeChange>().GetShrunkStatus())
                {
                    other.GetComponent<Rigidbody>().velocity = (Vector2.up * fanPowerForObjects * Time.deltaTime);
                }
            }


        }*/
    }
}
