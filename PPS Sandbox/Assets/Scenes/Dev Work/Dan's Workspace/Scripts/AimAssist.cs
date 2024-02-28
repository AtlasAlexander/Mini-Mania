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
    public Transform camTrans;

    [Header("Aim Assist")]
    public bool lookingAtObject;
    public float assistLookSpeedX;
    public float assistLookSpeedY;
    public LayerMask objectOfImportanceLayer;
    public float originalSensX;
    public float originalSensY;

    [SerializeField] GameObject growthRay;
    [SerializeField] GameObject shrinkRay;
    public AimAssistManager assman;

    private void Start()
    {
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        originalSensX = fpc.lookSpeedX;
        originalSensY = fpc.lookSpeedY;
    }


    private void Update()
    {
        HandleAimAssist();
        Debug.DrawRay(camTrans.position, camTrans.forward * 100, Color.red);

        //if (lookingAtObject)
        //{
        //    fpc.mouseLookSpeedX = originalSensX * 0.5f;
        //    fpc.mouseLookSpeedY = originalSensY * 0.5f;
        //}
        //
        //else if (!lookingAtObject)
        //{
        //    fpc.mouseLookSpeedX = originalSensX;
        //    fpc.mouseLookSpeedY = originalSensY;
        //}
    }

    private void HandleAimAssist()
    {
        int objectLayer = LayerMask.NameToLayer("ObjectOfImportance");
        if (Physics.SphereCast(camTrans.position, 0.15f, camTrans.forward, out RaycastHit hitRange, 1000))
        {
            var layerMask = hitRange.collider.gameObject.layer;
            if (layerMask == objectLayer)
            {
                if (growthRay.activeInHierarchy)
                {
                    reticle.color = new Color32(255, 155, 0, 255);
                }
                if (shrinkRay.activeInHierarchy)
                {
                    reticle.color = new Color32(126, 255, 227, 255);
                }
                assman.TestFunc();
                lookingAtObject = true;
            }

            else
            {
                lookingAtObject = false;
                reticle.color = Color.white;
            }
        }

        else
        {
            lookingAtObject = false;
            reticle.color = Color.white;
        }
        /*
        if (Physics.Raycast(camTrans.position, camTrans.forward, out RaycastHit hit, 1000))
        {
            var layerMask = hit.collider.gameObject.layer;
            if (layerMask == objectLayer)
            {
                if(growthRay.activeInHierarchy)
                {
                    reticle.color = new Color32(255, 155, 0, 255);
                }
                if(shrinkRay.activeInHierarchy)
                {
                    reticle.color = new Color32(126, 255, 227, 255);
                }
            }

            else
            {
                reticle.color = Color.white;
            }

        }
        */
    }
}
