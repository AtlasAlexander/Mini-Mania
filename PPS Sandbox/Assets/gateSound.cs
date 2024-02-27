using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("gatePassthrough", gameObject);
        }
    }
}
