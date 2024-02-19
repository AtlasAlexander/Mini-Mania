using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCutsceneClass : MonoBehaviour
{
    public GameObject objectCamPosition;
    public GameObject objectLookAtTarget;

    public GameObject playerCameraTarget;
    public GameObject playerCamPosition;

    private GameObject player;
    private Camera playerCamera;
    private GameObject playerWeapons;
    private GameObject crosshairUI;
    private GameObject audioManager;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        playerWeapons = GameObject.Find("Weapons");
        crosshairUI = GameObject.Find("IT_UI");
        audioManager = GameObject.Find("AudioManager");
    }

    void Start()
    {
    }


    //call this in dialogue script when player is talking
    public void PlayerCutsceneView()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        playerWeapons.SetActive(false);
        crosshairUI.SetActive(false);
        audioManager.SetActive(false);
        playerCamera.transform.position = playerCameraTarget.transform.position;
        playerCamera.transform.LookAt(playerCamPosition.transform);
    }

    //call this in dialogue script when other npc is talking
    public void ObjectCutsceneView()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        playerWeapons.SetActive(false);
        crosshairUI.SetActive(false);
        audioManager.SetActive(false);
        playerCamera.transform.position = objectCamPosition.transform.position;
        playerCamera.transform.LookAt(objectLookAtTarget.transform);
    }

    //call this when dialogue is finished and you want to go back to player's view
    public void ReturnCamToOrigin()
    {
        playerCamera.transform.position = playerCamPosition.transform.position;
        playerCamera.transform.rotation = playerCamPosition.transform.rotation;
        playerWeapons.SetActive(true);
        crosshairUI.SetActive(true);
        audioManager.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
