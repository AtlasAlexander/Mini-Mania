using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoSizeChanger : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            ChangeSize(0.2f);
        }

        if (Input.GetKeyDown("x"))
        {
            ChangeSize(1);
        }

        if (Input.GetKeyDown("c"))
        {
            ChangeSize(5);
        }
    }

    private void ChangeSize(float size)
    {
        player.transform.localScale = new Vector3(size, size, size);
    }
}
