using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Threading;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;                   //This Audio Manager is here to easily play sounds
                                             //Sounds from here will not be played in 3D space
    public static AudioManager instance;     //Play sounds from the Audio Manager with "FindObjectOfType<AudioManager>().Play("sound_name");"
                                             //This works from any script
    private bool muted;
    void Awake()
    {
        muted = false;

        if (instance == null)             //This code allows the audio manager to remain constant between scenes
            instance = this;              //and ensures there is only ever one Audio Manager
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)                   //Creates an audio source for each sound clip in the array
        {                                             //You can access the sounds array from the Audio Manager object in the inspector
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;               //Copies all the given information for the audio source.
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            FindObjectOfType<AudioListener>().enabled = muted;
            muted = !muted;
            
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //Find the sound in the sound array where sound.name = the string passed into Play()
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Stop();
    }


    public bool IsPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        return s.source.isPlaying;
    }

    public void SetPitch(string sound, float p)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        s.pitch = p;
    }

}
