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
    public float alpha;
    public bool disPlayTrack;
    public bool pausedMusic = false;

    public TextMeshProUGUI currentSong;
    public Image SongCover, Boarder;

    public PlayerControls playerControls;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();

        i = Random.Range(0, otherClip.Length);
    }

    private void LateUpdate()
    {
        if(!pausedMusic)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = otherClip[i];
                audioSource.Play();
                StartCoroutine(SongDisplay());
                i += 1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (i >= otherClip.Length)
        {
            i = 0;
        }
    }

    private void Update()
    {
        playerControls.Music.Skip.performed += x => songSkipped();
        playerControls.Music.Stop.performed += x => songStopped();

        Color TextColour = currentSong.color; 
        Color BoarderColour = Boarder.color;
        Color SongCoverColour = SongCover.color;
        if (disPlayTrack && alpha <= 1)
        {
            alpha += Time.deltaTime;
        }
        else if(alpha >= 0)
        {
            alpha -= Time.deltaTime;
        }
        
        SongCoverColour.a = alpha;
        BoarderColour.a = alpha;
        TextColour.a = alpha;
        SongCover.color = SongCoverColour;
        Boarder.color = BoarderColour;
        currentSong.color = TextColour;
    }

    private void songSkipped()
    {
        if(!pausedMusic)
        {
            audioSource.Stop();
        }
    }

    private void songStopped()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Pause();
            pausedMusic = true;
        }
        else
        {
            audioSource.Play();
            pausedMusic = false;
        }
    }
    IEnumerator SongDisplay()
    {
        currentSong.text = audioSource.clip.name;
        disPlayTrack = true;
        yield return new WaitForSeconds(5);
        disPlayTrack = false;
    }
}
