using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelOnlyCutsceneScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject weapons;
    CharacterController controller;
    
    void Awake()
    {
        controller = player.GetComponent<CharacterController>();
   
        controller.enabled = false;
        weapons.SetActive(false);

        
    }

    public void EnableMovement()
    {
        weapons.SetActive(true);
        controller.enabled = true; 
    }

 
}
