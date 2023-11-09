using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScale : MonoBehaviour
{
    public Transform leftSide;
    public Transform rightSide;

    void Update()
    {
        // Calculate the total mass on each side
        float leftMass = CalculateTotalMass(leftSide);
        float rightMass = CalculateTotalMass(rightSide);

        // Compare the masses and adjust the scale
        if (leftMass > rightMass)
        {
            // Rotate the scale to represent an imbalance to the left
            transform.rotation = Quaternion.Euler(0, 0, 10);
        }
        else if (rightMass > leftMass)
        {
            // Rotate the scale to represent an imbalance to the right
            transform.rotation = Quaternion.Euler(0, 0, -10);
        }
        else
        {
            // The scale is balanced
            transform.rotation = Quaternion.identity;
        }
    }

    float CalculateTotalMass(Transform side)
    {
        float totalMass = 0f;

        // Calculate the total mass of objects on a particular side of the scale
        foreach (Transform child in side)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                totalMass += rb.mass;
            }
        }

        return totalMass;
    }
}

