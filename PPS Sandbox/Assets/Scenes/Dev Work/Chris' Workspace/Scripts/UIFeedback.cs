using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFeedback : MonoBehaviour
{
    private Camera mainCamera;
    public RawImage pickupImageMouse;
    public RawImage mirrorImageMouse;
    private RaycastHit hit;
    public bool isActive = true;
    private Vector3 offset;
    private float offsetXPercentage = 0.05f;
    private float offsetYPercentage = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
        pickupImageMouse.enabled = false;
        mirrorImageMouse.enabled = false;
        offset = new Vector3(0, 10, 0);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        float offsetX = Screen.width * offsetXPercentage;
        float offsetY = Screen.height * offsetYPercentage;

        if (isActive)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Pickup"))
                {
                    pickupImageMouse.enabled = true;
                    pickupImageMouse.transform.position = new Vector3(offsetX, offsetY, 0);
                    pickupImageMouse.transform.localScale = new Vector3(1, 1.6f, 0);
                }
                else
                {
                    pickupImageMouse.enabled = false;
                }

                if (hit.collider.CompareTag("Mirror"))
                {
                    mirrorImageMouse.enabled = true;
                    mirrorImageMouse.transform.position = new Vector3(offsetX, offsetY, 0);
                    mirrorImageMouse.transform.localScale = new Vector3(1, 1.4f, 0);
                }
                else
                {
                    mirrorImageMouse.enabled = false;
                }
            }
        }
    }
}
