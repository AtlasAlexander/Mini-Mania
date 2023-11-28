using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCameraScript : MonoBehaviour
{
    private Transform trans;
    private Vector3 offset;

    private void Start()
    {
        trans = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        offset = (trans.rotation.eulerAngles  - transform.rotation.eulerAngles) ;
    }
    private void Update()
    {
        Quaternion rot = Quaternion.Euler(trans.rotation.eulerAngles - offset * -1.0f);
        gameObject.transform.rotation = rot;
    }
}
