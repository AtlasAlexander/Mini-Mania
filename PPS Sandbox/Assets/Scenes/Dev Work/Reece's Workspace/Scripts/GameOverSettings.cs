using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
 

public class GameOverSettings : MonoBehaviour
{
    [SerializeField] private GameObject fmodObject;
    private FmodAudioManager fmodAudio;

    private void Start()
    {
        fmodAudio = FindObjectOfType<FmodAudioManager>();
    }

    public void GoToMainMenu()
    {
        fmodAudio.killMusic();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
