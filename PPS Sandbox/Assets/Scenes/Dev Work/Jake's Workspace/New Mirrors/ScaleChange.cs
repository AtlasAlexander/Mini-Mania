using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    //Apply to non-carry cubes
    public GameObject objToCopy;
    private void Update()
    {
        transform.localScale = objToCopy.transform.localScale;
        transform.localPosition = objToCopy.transform.localPosition;
    }
}
