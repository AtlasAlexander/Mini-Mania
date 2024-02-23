using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        playerWeapons = GameObject.Find("Weapons");
        crosshairUI = GameObject.Find("IT_UI");
        audioManager = GameObject.Find("FmodAudioManager");

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
            StartCoroutine(fadeInOut.FadeOut(0));

            player.GetComponent<FirstPersonController>().enabled = false;
            playerWeapons.SetActive(false);
            crosshairUI.SetActive(false);
            audioManager.SetActive(false);
            playerCamera.transform.SetPositionAndRotation(Vector3.MoveTowards(playerCamera.transform.position, transitionCamPos.transform.position, 2.8f * Time.deltaTime),
                Quaternion.RotateTowards(playerCamera.transform.rotation, transitionCamPos.transform.rotation, 40 * Time.deltaTime));

            /*            player.transform.position = Vector3.MoveTowards(player.transform.position, transitionCamPos.transform.position, 2.7f * Time.deltaTime);
                        playerCamera.transform.rotation = Quaternion.RotateTowards(playerCamera.transform.rotation, transitionCamPos.transform.rotation, 35 * Time.deltaTime);
            */
            grandpa.transform.rotation = Quaternion.Euler(0, 0, 0);

            grandpaAnimationHashes.animator.SetBool(grandpaAnimationHashes.isIdleBool, true);

            if (playerCamera.transform.position == transitionCamPos.transform.position &&
                playerCamera.transform.rotation == transitionCamPos.transform.rotation)
            {
                StartCoroutine(fadeInOut.FadeIn(0));

                grandpa.transform.position = new Vector3(grandpa.transform.position.x - 15.5f, transitionCamPos.transform.position.y - 15, grandpa.transform.position.z + 26.8f);
                
                grandpaAnimationHashes.animator.SetBool(grandpaAnimationHashes.isPickingUpBool, true);

                //player.GetComponent<CapsuleCollider>().enabled = true;
                playerCamera.GetComponent<BoxCollider>().enabled = true;
                levelTransitionTrigger.levelTransitioning = false;
            }
        }

        if (pickup == true)
        {
            playerCamera.transform.parent = gameObject.transform;

            Destroy(playerCamera.GetComponent<BoxCollider>());

            playerCamera.transform.SetPositionAndRotation(Vector3.Lerp(playerCamera.transform.position, grandpaHandPos.transform.position, 0.5f * Time.deltaTime),
            Quaternion.Lerp(playerCamera.transform.rotation, grandpaHandPos.transform.rotation, 0.5f * Time.deltaTime));

            /*
                        player.transform.parent = gameObject.transform;
                        Destroy(GameObject.FindWithTag("Player").GetComponent<CapsuleCollider>());

                        player.transform.position = Vector3.Lerp(player.transform.position, grandpaHandPos.transform.position, 0.5f * Time.deltaTime);
                        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, grandpaHandPos.transform.rotation, 0.5f * Time.deltaTime);*/
        }

        if (grandpaAnimator.GetCurrentAnimatorStateInfo(0).IsName("PutDown(PickupReverse)"))
        {
            transitionCamPos.SetActive(true);
            grandpa.transform.position = Vector3.Lerp(grandpa.transform.position, new Vector3(grandpa.transform.position.x, grandpa.transform.position.y, grandpa.transform.position.z + 25), 2.5f * Time.deltaTime);

            if (pickup == false)
            {
                playerCamera.transform.parent = transitionCamPos.transform;

                playerCamera.transform.SetPositionAndRotation(Vector3.Lerp(playerCamera.transform.position, transitionCamPos.transform.position, 0.5f * Time.deltaTime),
                    Quaternion.Lerp(playerCamera.transform.rotation, transitionCamPos.transform.rotation, 0.5f * Time.deltaTime));

                /*                player.transform.parent = transitionCamPos.transform;

                                player.transform.position = Vector3.Lerp(player.transform.position, transitionCamPos.transform.position, 0.5f * Time.deltaTime);
                                playerCamera.transform.rotation = Quaternion.Lerp(player.transform.rotation, transitionCamPos.transform.rotation, 0.5f * Time.deltaTime);*/
            }

            StartCoroutine(fadeInOut.FadeOut(timeToWaitForFadeOut));

            if (fadeInOut.canvasGroup.alpha >= 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
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
