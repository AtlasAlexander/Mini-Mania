using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler_left_script : MonoBehaviour
{
    public GameObject ruler;
    public GameObject RightSide;
    public bool left_side_down;

    void Update()
    {
        if (left_side_down == true)
        {
            ruler.transform.Rotate(-50 * Time.deltaTime,0,0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            left_side_down = true;
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            left_side_down = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            RightSide.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
