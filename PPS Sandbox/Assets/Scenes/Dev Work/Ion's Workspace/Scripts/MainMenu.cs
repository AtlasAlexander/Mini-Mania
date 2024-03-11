using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Load Current Saved Level
        FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);
        
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName.Contains("MAIN MENU"))
        {
            //Workaround for the audiomanager being set to disabled in main menu
            FindObjectOfType<FmodAudioManager>().gameObject.GetComponent<FmodAudioManager>().enabled = true;
        }
    }

        public void LoadLevelSelect()
    {
        FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(1);
        
    }

    public void LoadMenu()
    {
        if(FindObjectOfType<FmodAudioManager>() != null)
            FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("gg");
    }
}
