using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastResSelect : MonoBehaviour
{
    private bool triggered = false;
    public int respoint;

    //Plays the sound for passing through a gate and tells the player once
    //a checkpoint has been created.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && triggered == false)
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("gatePassthrough", gameObject);
            triggered = true; //Means that the sound will only be played once.
        }
    }

}
