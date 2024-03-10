using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class FmodMusicManager : MonoBehaviour
{
    public EventReference[] tracks;
    public EventInstance songPlaying;

    [SerializeField]
    private int startingSong;


    private int songIndex;
    public bool paused;

    private float pauseTimer;

    private bool exitScene = false;
    private void Awake()
    {
        
        pauseTimer = 0f;
        songIndex = startingSong;
    }

    private void Update()
    {
        pauseTimer += Time.deltaTime;

        FMOD.Studio.PLAYBACK_STATE playbackState;        //Finds if a song is currently playing
        songPlaying.getPlaybackState(out playbackState);

        if(Input.GetKeyDown(KeyCode.I))
        {
            songPlaying.setVolume(0);
            //togglePause();                 //Temporary method of playing/pausing until we find another way
        }

        if (exitScene) songPlaying.setVolume(0);

        if (playbackState.ToString() == "STOPPED" && paused == false)   //Plays the next track when a song ends
        {
            songPlaying.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            songPlaying.clearHandle();
            songPlaying.release();
            GetComponent<Wiggle>().StartWiggle();
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("static", gameObject);
            songIndex = songIndex+ 1;
            if (songIndex > tracks.Length - 1)
            { songIndex = 0; }
            songPlaying = FMODUnity.RuntimeManager.CreateInstance(tracks[songIndex]);
            songPlaying.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(songPlaying, GetComponent<Transform>(), GetComponent<Rigidbody>());
        }
       
        
    }

    public void togglePause()
    {
        if (pauseTimer > 0.2f)
        {
            GetComponent<Wiggle>().StartWiggle();
            pauseTimer = 0.0f;
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("static", gameObject);
            if (paused)  //Unpausing the game
            {

                songPlaying = FMODUnity.RuntimeManager.CreateInstance(tracks[songIndex]);
                songPlaying.start();
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(songPlaying, GetComponent<Transform>(), GetComponent<Rigidbody>());
                paused = false;
            }
            else         //Pausing the game
            {
                songPlaying.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                songIndex = songIndex + 1;
                if (songIndex > tracks.Length - 1)
                { songIndex = 0; }
                paused = true;
            }
        }
    }

    public void killMusic()
    {
        FmodMusicManager[] scripts = FindObjectsOfType<FmodMusicManager>();

        // Call YourFunction on each instance
        foreach (FmodMusicManager script in scripts)
        {
            script.songPlaying.setVolume(0);
        }
        songPlaying.setVolume(0);
        
    }

}