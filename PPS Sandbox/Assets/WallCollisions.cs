using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("thump", FindObjectOfType<FirstPersonController>().gameObject);
        }
        if (other.gameObject.tag == "Mirror")
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("thump", FindObjectOfType<FirstPersonController>().gameObject);
        }
    }
}
