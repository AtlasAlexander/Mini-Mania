using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionCutscene : MonoBehaviour
{
    [SerializeField] private GameObject grandpa;
    [SerializeField] private GameObject grandpaHandPos;
    [SerializeField] private GameObject transitionCamPos;

    private FadeInOut fadeInOut;
    [SerializeField] private float timeToWaitForFadeIn = 0;
    [SerializeField] private float timeToWaitForFadeOut = 15;
    [SerializeField] private float timeToFade;
    [SerializeField] [Range(0.1f, 1)] private float fadeSpeed = 0.5f;

    private bool pickup;

    private GameObject player;
    private Camera playerCamera;
    private GameObject playerWeapons;
    private GameObject crosshairUI;
    private GameObject audioManager;
    [SerializeField] private Animator grandpaAnimator;

    private LevelTransitionTrigger levelTransitionTrigger;
    private GrandpaAnimationHashes grandpaAnimationHashes;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = player.GetComponentInChildren<Camera>();
        playerWeapons = GameObject.Find("Weapons");
        crosshairUI = GameObject.Find("IT_UI");
        audioManager = GameObject.Find("FmodAudioManager 1 1");

        levelTransitionTrigger = GameObject.Find("LevelTransitionTrigger").GetComponent<LevelTransitionTrigger>();
        grandpaAnimationHashes = gameObject.AddComponent<GrandpaAnimationHashes>();
        grandpaAnimationHashes.animator = grandpaAnimator;

        fadeInOut = FindObjectOfType<FadeInOut>();

        fadeInOut.timeToFade = timeToFade;
    }

    private void Start()
    {
        StartCoroutine(fadeInOut.FadeIn(timeToWaitForFadeIn));
        grandpaAnimationHashes.animator.SetBool(grandpaAnimationHashes.isSittingBool, true);
    }

    private void Update()
    {
        if (levelTransitionTrigger.levelTransitioning == true)
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            playerWeapons.SetActive(false);
            crosshairUI.SetActive(false);
            audioManager.SetActive(false);
            playerCamera.transform.SetPositionAndRotation(Vector3.MoveTowards(playerCamera.transform.position, transitionCamPos.transform.position, 0.8f * Time.deltaTime),
                Quaternion.RotateTowards(playerCamera.transform.rotation, transitionCamPos.transform.rotation, 20 * Time.deltaTime));

            //add some sort of delay here
            grandpa.transform.position = Vector3.MoveTowards(grandpa.transform.position, new Vector3(grandpa.transform.position.x, transitionCamPos.transform.position.y, grandpa.transform.position.z + 0.045f), 30 * Time.deltaTime);
            grandpa.transform.rotation = Quaternion.Euler(0, 0, 0);

            grandpaAnimationHashes.animator.SetBool(grandpaAnimationHashes.isIdleBool, true);

            if (playerCamera.transform.position == transitionCamPos.transform.position &&
                playerCamera.transform.rotation == transitionCamPos.transform.rotation)
            {
                grandpaAnimationHashes.animator.SetBool(grandpaAnimationHashes.isPickingUpBool, true);

                playerCamera.GetComponent<BoxCollider>().enabled = true;
                levelTransitionTrigger.levelTransitioning = false;
            }
        }

        if (pickup == true)
        {
            playerCamera.transform.parent = gameObject.transform;
            Destroy(GameObject.Find("Main Camera").GetComponent<BoxCollider>());

            playerCamera.transform.SetPositionAndRotation(Vector3.Lerp(playerCamera.transform.position, grandpaHandPos.transform.position, 0.5f * Time.deltaTime),
            Quaternion.Lerp(playerCamera.transform.rotation, grandpaHandPos.transform.rotation, 0.5f * Time.deltaTime));
        }

        if (grandpaAnimator.GetCurrentAnimatorStateInfo(0).IsName("PutDown(PickupReverse)"))
        {
            transitionCamPos.SetActive(true);

            if (pickup == false)
            {
                playerCamera.transform.parent = transitionCamPos.transform;

                playerCamera.transform.SetPositionAndRotation(Vector3.Lerp(playerCamera.transform.position, transitionCamPos.transform.position, 0.5f * Time.deltaTime),
                    Quaternion.Lerp(playerCamera.transform.rotation, transitionCamPos.transform.rotation, 0.5f * Time.deltaTime));
            }

            StartCoroutine(fadeInOut.FadeOut(timeToWaitForFadeOut));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            pickup = true;
        }

        if (other.CompareTag("PositionalObject"))
        {
            pickup = false;
        }
    }
}
