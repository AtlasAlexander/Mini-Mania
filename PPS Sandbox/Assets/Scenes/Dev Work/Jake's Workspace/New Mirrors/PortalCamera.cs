using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera, mirror, otherMirror;

    private void Update()
    {
        Vector3 localPlayer = playerCamera.position - otherMirror.position;
        transform.position = mirror.position + localPlayer;

        float angularDifference = Quaternion.Angle(mirror.rotation, otherMirror.rotation);
        Quaternion mirrorRotationDiff = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCamDirection = mirrorRotationDiff * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCamDirection, Vector3.up);
        transform.rotation = playerCamera.rotation;
        gameObject.GetComponent<Camera>().fieldOfView = playerCamera.GetComponent<Camera>().fieldOfView;
    }
}
