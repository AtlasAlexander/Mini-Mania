using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler_right_script : MonoBehaviour
{
    public GameObject ruler;
    public GameObject LeftSide;
    //public GameObject player;
    public bool right_side_down;

    void Update()
    {
        if (right_side_down == true)
        {
            ruler.transform.Rotate(50 * Time.deltaTime,0,0);
        }
        // if (ruler.transform.localRotation.eulerAngles.x = 27f)
        // {
        //     right_side_down = false;
        // }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            right_side_down = true;
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            right_side_down = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            LeftSide.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
