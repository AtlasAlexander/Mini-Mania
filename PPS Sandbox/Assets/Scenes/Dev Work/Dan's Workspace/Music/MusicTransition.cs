using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicTransition : MonoBehaviour
{
    public AudioClip[] otherClip;
    AudioSource audioSource;
    public int i;

    public TextMeshProUGUI currentSong;

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
            StartCoroutine(SongDisplay());
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

    IEnumerator SongDisplay()
    {
        currentSong.text = audioSource.clip.name;
        currentSong.enabled = true;
        yield return new WaitForSeconds(5);
        currentSong.enabled = false;
    }
}
