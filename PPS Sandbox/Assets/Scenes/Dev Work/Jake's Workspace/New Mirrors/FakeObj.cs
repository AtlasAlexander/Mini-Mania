using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeObj : MonoBehaviour
{
    public Transform originalObj, mirror, otherMirror;

    public bool playerCopy;

    private void Update()
    {
        Vector3 localObj = originalObj.position - otherMirror.position;
        transform.position = mirror.position + localObj;

        float angularDifference = Quaternion.Angle(mirror.rotation, otherMirror.rotation);
        Quaternion mirrorRotationDiff = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newDirection = mirrorRotationDiff * originalObj.forward;
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);

        if(playerCopy)
        {
            transform.localScale = originalObj.parent.localScale;
        }
        else
        {
            transform.localScale = originalObj.localScale;
        }
    }
}
