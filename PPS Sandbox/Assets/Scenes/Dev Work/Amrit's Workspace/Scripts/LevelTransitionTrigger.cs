using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionTrigger : MonoBehaviour
{
    [HideInInspector] public bool levelTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelTransitioning = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
