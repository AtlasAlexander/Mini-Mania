using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound                        //This class is used in the list of sounds within the Audio Manager
{                                         //This class gives each sound clip all the relevent information to be played
    public string name;                   //from an Audio Source. 

    public AudioClip clip;

    [Range(0f,1.0f)]
    public float pitch;

    [Range(0.1f, 1.0f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;            //Assigns each sound an audiosource to play from (Attatched to the Audio Manager)

}
