using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionCutscene : MonoBehaviour
{
    public GameObject grandpaHandPos;
    public GameObject originalCamPos;

    public Camera mainCamera;

    private FadeInOut fadeInOut;
    public float timeToWaitForFadeIn = 0;
    public float timeToWaitForFadeOut = 15;

    public float timeToFade;

    private bool pickup;

    [SerializeField] [Range(0.1f, 1)] private float fadeSpeed = 0.5f;


    private void Awake()
    {
        fadeInOut = FindObjectOfType<FadeInOut>();

        fadeInOut.timeToFade = timeToFade;
    }

    private void Start()
    {
        StartCoroutine(fadeInOut.FadeIn(timeToWaitForFadeIn));
    }

    private void Update()
    {
        if (pickup == true)
        {
            mainCamera.transform.parent = gameObject.transform;

            mainCamera.transform.SetPositionAndRotation(Vector3.Lerp(mainCamera.transform.position, grandpaHandPos.transform.position, 0.5f * Time.deltaTime),
                Quaternion.Lerp(mainCamera.transform.rotation, grandpaHandPos.transform.rotation, 0.5f * Time.deltaTime));

            Destroy(GameObject.Find("Main Camera").GetComponent<BoxCollider>());
        }

        if (pickup == false)
        {
            originalCamPos.SetActive(true);

            mainCamera.transform.parent = originalCamPos.transform;

            mainCamera.transform.SetPositionAndRotation(Vector3.Lerp(mainCamera.transform.position, originalCamPos.transform.position, 0.5f * Time.deltaTime), 
                Quaternion.Lerp(mainCamera.transform.rotation, originalCamPos.transform.rotation, 0.5f * Time.deltaTime));
        }

        StartCoroutine(fadeInOut.FadeOut(timeToWaitForFadeOut));

        // StartCoroutine(FadeOut());
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

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(15);

        //fadeInOut.FadeOut();
    }
}
