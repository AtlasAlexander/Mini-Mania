using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimAssist : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    public FirstPersonController fpc;
    public Image reticle;

    [Header("Aim Assist")]
    public bool lookingAtObject;
    public float assistLookSpeedX;
    public float assistLookSpeedY;
    public LayerMask objectOfImportanceLayer;

    private void Start()
    {
        fpc = GetComponent<FirstPersonController>();
    }


    private void Update()
    {
        HandleAimAssist();
    }

    private void HandleAimAssist()
    {
        int objectLayer = LayerMask.NameToLayer("ObjectOfImportance");
        if (Physics.SphereCast(Camera.main.transform.position, 0.15f, Camera.main.transform.forward, out RaycastHit hitRange, 1000))
        {
            var layerMask = hitRange.collider.gameObject.layer;
            if (layerMask == objectLayer)
            {
                lookingAtObject = true;
                fpc.lookSpeedX = assistLookSpeedX;
                fpc.lookSpeedY = assistLookSpeedY;
            }

            else
            {
                lookingAtObject = false;
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000))
        {
            var layerMask = hit.collider.gameObject.layer;
            if (layerMask == objectLayer)
            {
                reticle.color = Color.red;
            }

            else
            {
                reticle.color = Color.white;
            }

        }
    }
}
