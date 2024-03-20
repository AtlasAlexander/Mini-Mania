using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVomit : MonoBehaviour
{
    public GameObject activeCube, originalCube, assignedRoom;
    public ParticleSystem beam, electricity;

    private void FixedUpdate()
    {
        if (activeCube == null)
        {
            activeCube = Instantiate(originalCube, transform.position, transform.rotation);
        }
        else
        {
            if (originalCube.GetComponent<SizeChange>().startSmall)
            {
                activeCube.transform.localScale = new Vector3(.4f, .4f, .4f);
            }
            else
            {
                activeCube.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
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
        beam.Play();
        yield return new WaitForEndOfFrame();
        beam.Stop();
        yield return new WaitForSeconds(0.2f);
        electricity.Play();
        yield return new WaitForEndOfFrame();
        electricity.Stop();
    }
}
