using UnityEngine;

public class Wiggle : MonoBehaviour
{
    public float wiggleAngle = 45f;  
    public float wiggleSpeed = 10f;  
    public float wiggleDuration = 2f;  

    private Transform bodyTransform;
    private Quaternion originalRotation;
    private float wiggleTimer = 0f;
    private bool isWiggling = false;

    void Start()
    {
        bodyTransform = transform.Find("body");
        originalRotation = bodyTransform.localRotation;
        
    }

    void Update()
    {

        if (isWiggling)
        {

            wiggleTimer += Time.deltaTime;

            float lerpTime = Mathf.PingPong(wiggleTimer * wiggleSpeed / wiggleDuration, 1f);
            Quaternion targetRotation = Quaternion.Euler(wiggleAngle, 0f, 0f);
            bodyTransform.localRotation = Quaternion.Lerp(originalRotation, targetRotation, lerpTime);

            if (wiggleTimer >= wiggleDuration)
            {
                StopWiggle();
            }
        }
    }

    public void StartWiggle()
    {
        if (!isWiggling && bodyTransform != null)
        {
            isWiggling = true;
            wiggleTimer = 0f;

            Invoke("StopWiggle", wiggleDuration);
        }
    }

    private void StopWiggle()
    {
        if (bodyTransform != null)
        {

            bodyTransform.localRotation = originalRotation;
            isWiggling = false;
        }
    }
}
