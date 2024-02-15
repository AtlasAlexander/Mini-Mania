using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator Anim;
    public bool reverseDoor;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if(reverseDoor)
        {
            Anim.SetFloat("DoorStage", -1f);
        }
        else
        {
            
            Anim.SetFloat("DoorStage", 1f);
        }
    }

    public void CloseDoor()
    {
        if (reverseDoor)
        {
            Anim.SetFloat("DoorStage", 1f);
        }
        else
        {
            Anim.SetFloat("DoorStage", -1f);
        }
    }
}
