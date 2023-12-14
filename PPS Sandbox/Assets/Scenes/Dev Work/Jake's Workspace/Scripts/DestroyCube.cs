using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public GameObject Dispenser;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goop")
        {
            transform.position = Dispenser.transform.position;
            if(Dispenser.GetComponent<CubeVomit>().originalCube.GetComponent<SizeChange>().startSmall)
            {
                transform.localScale = new Vector3(.2f, .2f, .2f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }
}
