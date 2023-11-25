using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioClip[] otherClip;
    AudioSource audioSource;
    public int i;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = otherClip[i];
            audioSource.Play();
            i += 1;
        }
    }

    private void FixedUpdate()
    {
        if (i > otherClip.Length)
        {
            i = 0;
        }
    }
}
