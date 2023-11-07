using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenDoor()
    {
        Anim.SetFloat("DoorStage", 1f);
    }

    void CloseDoor()
    {
        Anim.SetFloat("DoorStage", -1f);
    }
}
