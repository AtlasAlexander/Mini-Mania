using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class laser : MonoBehaviour
{

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player killed");
            Destroy(other.gameObject);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lr.SetPosition(1, hit.point);
                //Kill player
                Destroy(hit.collider.gameObject);
            }
            else
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }
}
