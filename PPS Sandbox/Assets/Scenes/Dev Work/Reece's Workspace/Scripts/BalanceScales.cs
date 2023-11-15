using UnityEngine;

public class BalanceScales : MonoBehaviour
{
    public GameObject scale1;
    public GameObject scale2;
    public GameObject balanceBeam;

    void Start()
    {
        // Set up the scales and balance beam as before

        // Example: Adding objects to the scales
        AddObjectToScale(scale1, 1.0f);
        AddObjectToScale(scale2, 0.5f);
    }

    void AddObjectToScale(GameObject scale, float weight)
    {
        GameObject obj = new GameObject("WeightObject");
        obj.transform.position = scale.transform.position + new Vector3(0, 1, 0);

        Rigidbody objRigidbody = obj.AddComponent<Rigidbody>();
        objRigidbody.mass = weight;

        FixedJoint fixedJoint = obj.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = scale.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check the total mass on each scale
        float massOnScale1 = GetTotalMass(scale1);
        float massOnScale2 = GetTotalMass(scale2);

        // Adjust the position of the scales based on the difference in mass
        float positionDifference = massOnScale1 - massOnScale2;
        float scaleMoveSpeed = 0.1f; // Adjust as needed

        // Move scales based on the difference in mass
        MoveScale(scale1, -positionDifference * scaleMoveSpeed);
        MoveScale(scale2, positionDifference * scaleMoveSpeed);
    }

    float GetTotalMass(GameObject scale)
    {
        Rigidbody[] rigidbodies = scale.GetComponentsInChildren<Rigidbody>();
        float totalMass = 0;

        foreach (Rigidbody rb in rigidbodies)
        {
            totalMass += rb.mass;
        }

        return totalMass;
    }

    void MoveScale(GameObject scale, float moveAmount)
    {
        scale.transform.position += new Vector3(moveAmount, 0, 0);
    }
}