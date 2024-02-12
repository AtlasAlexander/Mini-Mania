using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{

    public AudioMixer mainAudioMixer;
    public AudioMixer musicAudioMixer;
    public AudioMixer SFXAudioMixer;

    public Sound[] sounds;
    public AudioSource menuAuidos;

    private FmodAudioManager audioManager;

    private void Start()
    {
        audioManager = FindAnyObjectByType<FmodAudioManager>();

        sounds = FindObjectOfType<AudioManager>().sounds;
        menuAuidos = FindObjectOfType<PauseMenu>().GetComponent<AudioSource>();
    }
    public void SetMainVolume (float volume)
    {

    }

    public void SetMusicVolume (float volume)
    {
        musicAudioMixer.SetFloat("volume", volume);
    }

    public void SetSFXVolume (float volume)
    {
        foreach (Sound s in sounds)                   //Creates an audio source for each sound clip in the array
        {                                             //You can access the sounds array from the Audio Manager object in the inspector
            s.source.volume = volume;               //Copies all the given information for the audio source.
            print(s.source.volume);
        }

        menuAuidos.volume = volume; 
    }

    public void Mute(bool muted)
    {
        if (muted)
        {
            foreach (Sound s in sounds)                   //Creates an audio source for each sound clip in the array
            {                                             //You can access the sounds array from the Audio Manager object in the inspector
                s.source.mute = true;             //Copies all the given information for the audio source.
            }

            menuAuidos.mute = true;
        }

        else
        {
            foreach (Sound s in sounds)                   //Creates an audio source for each sound clip in the array
            {                                             //You can access the sounds array from the Audio Manager object in the inspector
                s.source.mute = false;             //Copies all the given information for the audio source.
            }

            menuAuidos.mute = false;
        }
    }
}
