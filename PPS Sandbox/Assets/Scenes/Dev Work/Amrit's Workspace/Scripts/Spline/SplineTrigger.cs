using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    private Camera playerCamera;
    private bool inTrigger;
    private float cameraSplineStart = 0;
    public SplineCamera splineCamera;
    private GameObject player;

    public FirstPersonController playerController;

    public BezierSpline spline;
    public GameObject playerTarget;

    private void Awake()
    {
        playerCamera = GameObject.Find("IT_Player").GetComponentInChildren<Camera>();
        player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        if (inTrigger)
        {
            // move the players camera to start of the spline
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, spline.GetPoint(splineCamera.progress), Time.deltaTime * 10);

            // look at player
            playerCamera.transform.LookAt(playerTarget.transform);
        }
        else
        {
            // look at player
            playerCamera.transform.LookAt(playerTarget.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
           // GameObject.Find("Player Camera Manager").GetComponent<RPGCameraController>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        inTrigger = false;
        //GameObject.Find("Player Camera Manager").GetComponent<RPGCameraController>().enabled = true;
    }
}
