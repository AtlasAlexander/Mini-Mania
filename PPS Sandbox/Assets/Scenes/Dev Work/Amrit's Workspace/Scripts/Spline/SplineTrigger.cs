using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    private Camera playerCamera;
    private GameObject player;

    public SplineWalker splineWalker;
    public BezierSpline spline;

    public GameObject lookAtTarget;
    public GameObject playerCamPosition;

    private GameObject playerWeapons;
    private GameObject crosshairUI;
    private GameObject audioManager;

    public GameObject nextSplineCutscene;

    private bool returnCamToOrigin = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        playerWeapons = GameObject.Find("Weapons");
        crosshairUI = GameObject.Find("IT_UI");
        audioManager = GameObject.Find("AudioManager");
    }

    private void LateUpdate()
    {

        if (splineWalker.active == true)
        {
            returnCamToOrigin = false;

            // move the players camera to start of the spline
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, spline.GetPoint(splineWalker.progress), Time.deltaTime * 10);

            // look at player
            playerCamera.transform.LookAt(lookAtTarget.transform);

            //disable player movement and weapons
            player.GetComponent<FirstPersonController>().enabled = false;
            playerWeapons.SetActive(false);
            crosshairUI.SetActive(false);
            audioManager.SetActive(false);

            if (splineWalker.progress >= 0.98f)
            {
                returnCamToOrigin = true;
            }
        }
        else
        
            //move camera back to player view

            if (returnCamToOrigin == true)
            {
                playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, playerCamPosition.transform.position, Time.deltaTime * 10);
            }

            //enable player movement and weapons

            if (playerCamera.transform.position == playerCamPosition.transform.position)
            {
                player.GetComponent<FirstPersonController>().enabled = true;
                playerWeapons.SetActive(true);
                crosshairUI.SetActive(true);
                audioManager.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            splineWalker.active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nextSplineCutscene.SetActive(true);
            Destroy(gameObject.GetComponentInParent<Transform>().parent.gameObject);
        }
    }
}
