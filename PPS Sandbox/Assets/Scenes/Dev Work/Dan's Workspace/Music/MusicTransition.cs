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
    public bool disPlayTrack;

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
        if (i >= otherClip.Length)
        {
            i = 0;
        }
    }

    private void Update()
    {
        playerControls.Music.Skip.performed += x => songSkipped();

        Color TextColour = currentSong.color; 
        Color BoarderColour = Boarder.color;
        Color SongCoverColour = SongCover.color;
        if (disPlayTrack)
        {
            SongCoverColour.a += Time.deltaTime;
            BoarderColour.a += Time.deltaTime;
            TextColour.a += Time.deltaTime / 1.1f;
        }
        else
        {
            SongCoverColour.a -= Time.deltaTime;
            BoarderColour.a -= Time.deltaTime;
            TextColour.a -= Time.deltaTime * 1.1f;
        }
        SongCover.color = SongCoverColour;
        Boarder.color = BoarderColour;
        currentSong.color = TextColour;
    }

    private void songSkipped()
    {
        audioSource.Stop();
    }
    IEnumerator SongDisplay()
    {
        currentSong.text = audioSource.clip.name;
        disPlayTrack = true;
        yield return new WaitForSeconds(5);
        disPlayTrack = false;
    }
}
