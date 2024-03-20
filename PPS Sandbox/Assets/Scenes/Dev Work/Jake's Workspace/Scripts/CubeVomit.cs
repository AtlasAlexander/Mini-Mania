using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVomit : MonoBehaviour
{
    public GameObject activeCube, originalCube, assignedRoom;
    public ParticleSystem beam, electricity;

    private void Awake()
    {
        StartCoroutine(CubeVomParticalEvent());
    }
    private void FixedUpdate()
    {
        if (activeCube == null)
        {
            activeCube = Instantiate(originalCube, transform.position, transform.rotation);
        }
        else
        {
            activeCube.GetComponent<DestroyCube>().Dispenser = gameObject;
            activeCube.GetComponent<DestroyCube>().AssignedRoom = assignedRoom;
        }
    }

    public void particleTriggerEvent()
    {
        StartCoroutine(CubeVomParticalEvent());
    }
    public IEnumerator CubeVomParticalEvent()
    {
        yield return new WaitForSeconds(0.2f);
        beam.Play();
        yield return new WaitForEndOfFrame();
        beam.Stop();
        yield return new WaitForSeconds(0.2f);
        electricity.Play();
        yield return new WaitForEndOfFrame();
        electricity.Stop();
    }
}
