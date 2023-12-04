using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Launcher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectile;
    public float launchSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            projectile.GetComponent<Rigidbody>().velocity = launchSpeed * launchPoint.up;
        }
    }
}
