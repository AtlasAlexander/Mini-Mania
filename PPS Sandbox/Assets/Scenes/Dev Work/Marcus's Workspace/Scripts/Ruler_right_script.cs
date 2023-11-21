using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler_right_script : MonoBehaviour
{
    public GameObject ruler;
    public GameObject player;
    public bool right_side_down;

    void Update()
    {
        if (right_side_down == true)
        {
            //ruler.transform.rotation.x * Time.deltaTime * 5f;
            ruler.transform.Translate(transform.rotate.x * Time.deltaTime * 5);
        }
    }
}
