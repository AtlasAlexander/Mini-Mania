using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("REFERENCES")]
    public GameObject[] MainMenuOptions;
    public GameObject[] camLocations;
    public GameObject[] levelText;
    public GameObject[] buttons;
    public Transform mainCamPos;
    Light levelSelectLight;
    PlayerControls playerControls;
    public GameObject startText;
    Camera cam;

    [Header("VARIABLES")]
    public bool startPressed = false;
    public float dis = 0;
    public bool levelSelect = false;
    public float speed = 1;
    public int i = 0;
    public bool isMoving;

    private void Awake()
    {
        playerControls = new PlayerControls();
        i = 0;
        startPressed = false;
        levelSelect = false;
        dis = 0;
        mainCamPos = GameObject.Find("MenuCamPos").transform;
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        cam = Camera.main;
        foreach (GameObject button in MainMenuOptions)
        {
            button.SetActive(false);
        }

        foreach (GameObject arrows in buttons)
        {
            arrows.SetActive(false);
        }
        levelSelectLight = cam.GetComponent<Light>();
        levelSelectLight.enabled = false;
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName.Contains("MAIN MENU"))
        {
            if (startPressed && !levelSelect)
            {
                if (playerControls.Actions.NavigateMenu.WasPressedThisFrame())
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("navigateMenu", FindObjectOfType<Camera>().gameObject);
                }
            }
            //Workaround for the audiomanager being set to disabled in main menu
            FindObjectOfType<FmodAudioManager>().gameObject.GetComponent<FmodAudioManager>().enabled = true;
        }

        if (cam.transform.hasChanged)
        {
            isMoving = true;
        }

        else
        {
            isMoving = false;
        }

        float moveSpeed = Time.deltaTime * speed;
        if (playerControls.Actions.Play.IsPressed() && !startPressed)
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("enterMenu", FindObjectOfType<Camera>().gameObject);
            startPressed = true;
            foreach (GameObject button in MainMenuOptions)
            {
                button.SetActive(true);
                startText.SetActive(false);
            }
        }

        dis = Vector3.Distance(cam.transform.position, mainCamPos.position);
        if (dis > 0 && startPressed && !levelSelect)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, mainCamPos.position, moveSpeed);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, mainCamPos.rotation, moveSpeed * 10);
        }

        float dirPressed = playerControls.Actions.NavigateMenu.ReadValue<Vector2>().x;
        if (levelSelect)
        {
            EventSystem.current.SetSelectedGameObject(buttons[2]);
            
            if(i >= PlayerPrefs.GetInt("Level"))
                levelSelectLight.enabled = false;
            else
                levelSelectLight.enabled = true;
            cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[i].transform.position, moveSpeed);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, camLocations[i].transform.rotation, moveSpeed * 10);
            levelText[i].SetActive(true);
            foreach (GameObject arrows in buttons)
            {
                arrows.SetActive(true);
            }

            if (playerControls.Actions.NavigateMenu.WasPressedThisFrame() && dirPressed > 0)
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", FindObjectOfType<Camera>().gameObject);
                i += 1;
                levelText[i - 1].SetActive(false);
            }

            if (playerControls.Actions.NavigateMenu.WasPressedThisFrame() && dirPressed < 0)
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", FindObjectOfType<Camera>().gameObject);
                i -= 1;
                levelText[i + 1].SetActive(false);
            }


            if (playerControls.Actions.Pause.IsPressed() || playerControls.Actions.Return.IsPressed())
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("enterMenu", FindObjectOfType<Camera>().gameObject);
                levelSelect = !levelSelect;
                levelText[i].SetActive(false);
                levelSelectLight.enabled = false;
                foreach (GameObject arrows in buttons)
                {
                    arrows.SetActive(false);
                }
            }


        }

        if (i >= camLocations.Length)
        {
            i = 0;
        }

        if (i < 0)
        {
            i = camLocations.Length - 1;
        }

    }
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Load Current Saved Level
        FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);       
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("Level", 1);
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

    public void LevelSelect()
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("exitMenu", FindObjectOfType<Camera>().gameObject);
        levelSelect = !levelSelect;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("gg");
    }

    public void FowardLevel()
    {
        i += 1;
        levelText[i - 1].SetActive(false);
        if (i >= camLocations.Length)
        {
            i = 0;
        }
    }
    public void BackLevel()
    {
        i -= 1;
        levelText[i + 1].SetActive(false);
        if (i < 0)
        {
            i = camLocations.Length - 1;
        }
        
    }

    public void ExitLevelSelect()
    {
        levelSelect = false;
        levelText[i].SetActive(false);
        levelSelectLight.enabled = false;
        foreach (GameObject arrows in buttons)
        {
            arrows.SetActive(false);
        }
    }

    public void PlaySelected()
    {
        if (levelSelectLight.enabled == true)
        {
            SceneManager.LoadScene(i + 2);
        }
    }

}
