using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FmodAudioManager : MonoBehaviour
{
    public EventReference[] gameplaySounds;     //Creates the array of sounds so that sounds can be easily added from the inspector
     
    [SerializeField] float footstepsRate;       //changes the speed of footsteps
    [SerializeField] GameObject player;         
    [SerializeField] FirstPersonController controller;

    float time;

    public void QuickPlaySound(string soundName, GameObject soundSource) 
    {
        //Use this function form any script to play a sound
        //Use the name of the sound in Assets/Sounds for soundName
        //Pass in the object you want the sound to play from into soundSource

        RuntimeManager.PlayOneShotAttached(gameplaySounds[FindEventReferenceByName(soundName)], soundSource);     
    }

    public int FindEventReferenceByName(string eventName)  //Finds the position of a sound name in the gameplaySounds Array
    {
        int soundIndex = 0;
        foreach (EventReference eventRef in gameplaySounds)
        {
            string soundName = eventRef.Path.Replace("event:/GameSoundEffects/", "");
            if (soundName == eventName)
            { 
                return soundIndex;
            }
            soundIndex++;
        }
        Debug.Log("Sound: " + eventName + " not found in the gameplaySounds array.");
        return -1;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (controller.isWalking)    //controls player footsteps
        {
            if (time >= footstepsRate)
            {
                QuickPlaySound("footsteps", player);
                time = 0;
            }
        }
    }
        
}
