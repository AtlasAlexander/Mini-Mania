using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    private Camera playerCamera;
    public SplineWalker splineWalker;
    private GameObject player;

    public BezierSpline spline;
    public GameObject playerTarget;

    public GameObject playerWeapons;

    public GameObject playerCamPosition;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        playerWeapons = GameObject.Find("Weapons");
    }

    private void LateUpdate()
    {

        if (splineWalker.active == true)
        {
            // move the players camera to start of the spline
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, spline.GetPoint(splineWalker.progress), Time.deltaTime * 10);

            // look at player
            playerCamera.transform.LookAt(playerTarget.transform);

            //disable player movement and weapons
            player.GetComponent<FirstPersonController>().enabled = false;
            playerWeapons.SetActive(false);
        }
        else
        {
            //move camera back to player view
            playerCamera.transform.position = Vector3.MoveTowards(playerCamera.transform.position, playerCamPosition.transform.position, Time.deltaTime * 10);

            //enable player movement and weapons

            if (playerCamera.transform.position == playerCamPosition.transform.position)
            {
                player.GetComponent<FirstPersonController>().enabled = true;
                playerWeapons.SetActive(true);
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
            Destroy(gameObject.GetComponentInParent<Transform>().parent.gameObject);
        }
    }
}
