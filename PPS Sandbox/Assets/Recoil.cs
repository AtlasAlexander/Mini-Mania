using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    public float recoilIntensity = 0.1f;
    public Vector3 recoilDirection = new Vector3(0, 0, -1);
    public float recoilRecoverySpeed = 1f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void RecoilInit()
    {
        //initial recoil
        transform.Translate(recoilDirection * recoilIntensity, Space.Self);


        //Start coroutine to recover from recoil
        StartCoroutine(RecoverFromRecoil());
    }

    private IEnumerator RecoverFromRecoil()
    {
        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
        {
            // Move towards original position gradually
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, recoilRecoverySpeed * Time.deltaTime);
            yield return null;
        }
        // Return bacl to the original position
        transform.localPosition = originalPosition;
    }
}
