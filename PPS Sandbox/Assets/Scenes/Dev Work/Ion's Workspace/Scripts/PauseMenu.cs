using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject controlsMenuUI;
    public GameObject keyboardControlsUI;
    public GameObject gamepadControlsUI;
    //MusicTransition musicTransition;
    //AudioSource audioSource;
    //public AudioClip[] audio;

    PlayerControls playerControls;

    [Header("First Selected Options")]
    [SerializeField] private GameObject pauseMenuFirst;
    [SerializeField] private GameObject pauseOptionsMenuFirst;
    [SerializeField] private GameObject controlsMenuFirst;
    [SerializeField] private GameObject gamepadMenuFirst;
    [SerializeField] private GameObject keyboardMenuFirst;


    // Update is called once per frame

    private void Start()
    {
        //musicTransition = FindObjectOfType<MusicTransition>();
        //audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    void Update()
    {
        playerControls.Actions.Pause.performed += x => HandlePause();
        
    }

    void HandlePause()
    {
       
        if (GamePaused)
         {
            Resume();
        }

        else
        {
            Pause();
            //audioSource.clip = audio[0];
            //audioSource.Play();
        }
    }

    public void Transition()
    {
        //FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", GameObject.FindWithTag("Player").gameObject);
        //audioSource.clip = audio[1];
        //audioSource.Play();
    }

    public void Resume ()
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("pause", GameObject.FindWithTag("Player").gameObject);
        //musicTransition.songStopped();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        keyboardControlsUI.SetActive(false);
        gamepadControlsUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause ()
    {
        
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("pause", GameObject.FindWithTag("Player").gameObject);
        //musicTransition.songStopped()
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MainPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
    }

    public void OptionsMenu()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(pauseOptionsMenuFirst);
    }

    public void MainMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
    }

    public void ControlsMenu()
    {
        EventSystem.current.SetSelectedGameObject(controlsMenuFirst);
    }

    public void GamepadMenu()
    {
        EventSystem.current.SetSelectedGameObject(gamepadMenuFirst);
    }

    public void KeyboardMenu()
    {
        EventSystem.current.SetSelectedGameObject(keyboardMenuFirst);
    }




    public void ResetScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitThenReset(0.1f));
    }

    IEnumerator WaitThenReset(float time)
    {
        yield return new WaitForSeconds(time);
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        //Debug.Log("Exit game");
        //Application.Quit();
    }
}
