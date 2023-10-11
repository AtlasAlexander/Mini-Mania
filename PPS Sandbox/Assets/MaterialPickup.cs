using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialPickup : MonoBehaviour
{
    public TextMeshProUGUI materialsText;
    //amount of mats needed to build gun (edit in inspector)
    public int materialsNeeded;
    int materialsCollected = 0;

    private void Update()
    {
        //Update the amount of mats collected on UI
        materialsText.text = "Mats Collected: " + materialsCollected.ToString();
    }

    //Called when player collides with another object
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If player collides with a material
        if (hit.gameObject.CompareTag("Material"))
        {
            //make it disappear and add to amount collected
            Destroy(hit.gameObject);
            materialsCollected++;
        }
    }
}
