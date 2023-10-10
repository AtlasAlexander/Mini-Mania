using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
//using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] InputAction zoom;
    //[SerializeField] Camera fpsCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    //[SerializeField] FirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 80f;
    [SerializeField] float zoomedInFov = 30f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.5f;

    
    private void OnEnable()
    {
        zoom.Enable();
    }

    private void OnDisable()
    {
        zoom.Disable();
        ZoomOut();
    }

    private void Update()
    {
        if(zoom.ReadValue<float>() > 0.5)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView = zoomedInFov;
        //fpsController.RotationSpeed = zoomInSensitivity;
        //fpsCamera.fieldOfView = zoomedInFov;
    }

    private void ZoomOut()
    {
        cinemachineVirtualCamera.m_Lens.FieldOfView = zoomedOutFOV;
        //fpsController.RotationSpeed = zoomOutSensitivity;
    }
}
