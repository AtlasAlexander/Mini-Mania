using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirroredObj : MonoBehaviour
{
    public Transform objToCopy;
    public Transform mirror;
    void Update()
    {
        Vector3 localObj = mirror.InverseTransformPoint(objToCopy.position);
        transform.position = mirror.TransformPoint(new Vector3(localObj.x, -localObj.y, localObj.z));
        transform.localScale = objToCopy.localScale;
    }
}
