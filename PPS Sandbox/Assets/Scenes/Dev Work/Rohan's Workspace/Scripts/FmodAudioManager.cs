using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FMOD.Studio;
using System;
using System.Data.SqlTypes;

public class FmodAudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0.0f, 1.0f)]
    public float soundEffectsVolume;
    //[Range(0, 1)]
    //public float musicVolume = 1;

    private Bus sfxBus;
    private Bus musicBus;

    public EventReference[] gameplaySounds;     //Creates the array of sounds so that sounds can be easily added from the inspector
     
    [SerializeField] float footstepsRate;       //changes the speed of footsteps
    [SerializeField] GameObject player;         
    [SerializeField] FirstPersonController controller;

    float time;

    private void Awake()
    {
        sfxBus = RuntimeManager.GetBus("bus:/");
        //musicBus = RuntimeManager.GetBus("bus:/Music");
    }

    private void Start()
    {
        QuickPlaySound("roomAmbience", player);
    }

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
            //string soundName = eventRef.Path.Replace("event:/GameSoundEffects/", "");
            //if (soundName == eventName)
            // {
            if (eventRef.ToString().Contains(eventName))
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
        sfxBus.setVolume(soundEffectsVolume);
        //musicBus.setVolume(musicVolume);

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


    public void SetFootstepsRate(float rate)
    {

        footstepsRate = rate;
    }
}
