using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimAssist : MonoBehaviour
{
    [Header("References")]
    private GameObject player;
    public FirstPersonController fpc;
    private GameObject reticleObj;
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
        reticleObj = GameObject.Find("Reticle");
        reticle = reticleObj.GetComponent<Image>();
        originalSensX = fpc.lookSpeedX;
        originalSensY = fpc.lookSpeedY;
        camTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }


    private void Update()
    {
        HandleAimAssist();
        Debug.DrawRay(camTrans.position, camTrans.forward * 100, Color.red);

        if (lookingAtObject)
        {
            if (growthRay.activeInHierarchy)
            {
                //reticle.color = new Color32(255, 155, 0, 255);
                reticle.color = Color.red;
                
            }
            if (shrinkRay.activeInHierarchy)
            {
                //reticle.color = new Color32(126, 255, 227, 255);
                reticle.color = Color.cyan;
                
            }
        }

        else
        {
            reticle.color = Color.white;
        }
    }

    private void HandleAimAssist()
    {
        int objectLayer = LayerMask.NameToLayer("ObjectOfImportance");
        if (Physics.SphereCast(camTrans.position, 0.15f, camTrans.forward, out RaycastHit hitRange, 1000))
        {
            var layerMask = hitRange.collider.gameObject.layer;
            if (layerMask == objectLayer)
            {
                assman.TestFunc();
                lookingAtObject = true;
            }

            else
            {
                lookingAtObject = false;
            }
        }
    }
}
