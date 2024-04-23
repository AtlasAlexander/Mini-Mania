using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelOnlyCutsceneScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    CharacterController controller;
    
    void Awake()
    {
        controller = player.GetComponent<CharacterController>();
   
        controller.enabled = false;
    }

    public void EnableMovement()
    {
        controller.enabled = true; 
    }

 
}
