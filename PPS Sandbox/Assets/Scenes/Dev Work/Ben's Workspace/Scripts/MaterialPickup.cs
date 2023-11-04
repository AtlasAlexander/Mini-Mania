using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialPickup : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI materialsText;
    [SerializeField] private GameObject pressE;
    private GameObject growthRay;
    [SerializeField] private GameObject FinishedGrowthRay;
    private bool rayBuilt = false;

    //amount of mats needed to build gun (edit in inspector)
    public int materialsNeeded;
    int materialsCollected = 0;

    private void Start()
    {
        growthRay = GameObject.FindGameObjectWithTag("GrowthRay");
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        //Update the amount of mats collected on UI
        //materialsText.text = "Mats Collected: " + materialsCollected.ToString();
        //if (!rayBuilt)
        //{
        //    if (materialsCollected >= materialsNeeded && Vector3.Distance(gameObject.transform.position, growthRay.transform.position) < 5)
        //    {
        //        pressE.SetActive(true);
        //        if (inputManager.GetInteraction() > 0)
        //        {
        //            Instantiate(FinishedGrowthRay, growthRay.transform.position, Quaternion.identity);
        //            Destroy(growthRay);
        //            rayBuilt = true;
        //        }
        //    }
        //    else
        //        pressE.SetActive(false);
        //}
        //else
        //    pressE.SetActive(false);
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
