using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("REFERENCES")]
    public GameObject MainMenuCanvas;
    public GameObject optionMenuCanvas;
    public GameObject levelSelectCanvas;
    public GameObject[] camLocations;
    public GameObject[] levelText;
    public Transform mainCamPos;
    public Transform optionsCamPos;
    Light levelSelectLight;
    PlayerControls playerControls;
    public GameObject startText;
    Camera cam;

    [Header("VARIABLES")]
    public bool startPressed = false;
    public float dis = 0;
    public bool mainMenu = false;
    public bool optionsMenu = false;
    public bool levelSelect = false;
    public float speed = 1;
    public int i = 0;
    public bool isMoving;
    float moveSpeed;
    public bool transition = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        i = 0;
        startPressed = false;
        levelSelect = false;
        dis = 0;
        mainCamPos = GameObject.Find("MenuCamPos").transform;
        optionsCamPos = GameObject.Find("OptionsCamPos").transform;
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
        optionMenuCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
        startText.SetActive(true);
        levelSelectCanvas.SetActive(false);
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
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("navigateMenu", GameObject.Find("MenuListener").gameObject);
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
        moveSpeed = Time.deltaTime * speed;
        if (playerControls.Actions.Play.IsPressed() && !startPressed && !optionsMenu)
        {
            FindObjectOfType<FmodAudioManager>().QuickPlaySound("enterMenu", GameObject.Find("MenuListener").gameObject);
            startPressed = true;
            startText.SetActive(false);
            mainMenu = true;
            SetSelected("New Game");
        }

        dis = Vector3.Distance(cam.transform.position, mainCamPos.position);
        if (dis > 0 && startPressed && !levelSelect && !optionsMenu)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, mainCamPos.position, moveSpeed);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, mainCamPos.rotation, moveSpeed * 10);
        }

        if (mainMenu)
        {
            HandleMainMenu();
        }

        if (levelSelect)
        {
            HandleLevelSelect();
        }

        if (optionsMenu)
        {
            HandleOptionMenu();
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
        FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);       
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("Level", 1);
    }

    public void ToggleMainMenu()
    {
        mainMenu = true;
        levelSelect = false;
        optionsMenu = false;

        optionMenuCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void ToggleOptionMenu()
    {
        optionsMenu = true;
        mainMenu = false;
        MainMenuCanvas.SetActive(false);
        optionMenuCanvas.SetActive(true);
        
    }

    public void LoadMenu()
    {
        if(FindObjectOfType<FmodAudioManager>() != null)
            FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(0);
    }

    public void LevelSelect()
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("pause", FindObjectOfType<Camera>().gameObject);
        levelSelect = true;
        mainMenu = false;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("gg");
    }

    public void FowardLevel()
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", GameObject.Find("MenuListener").gameObject);
        i += 1;
        levelText[i - 1].SetActive(false);
        if (i >= camLocations.Length)
        {
            i = 0;
        }
    }
    public void BackLevel()
    {
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("menuSelection", GameObject.Find("MenuListener").gameObject);
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
        mainMenu = true;
        levelText[i].SetActive(false);
        levelSelectLight.enabled = false;
        levelSelectCanvas.SetActive(false);
        FindObjectOfType<FmodAudioManager>().QuickPlaySound("enterMenu", GameObject.Find("MenuListener").gameObject);
    }

    public void PlaySelected()
    {
        if (levelSelectLight.enabled == true)
        {
            SceneManager.LoadScene(i + 2);
        }
    }

    private void HandleMainMenu()
    {
        optionsMenu = false;
        levelSelect = false;
        MainMenuCanvas.SetActive(true);
    }

    private void HandleLevelSelect()
    {
        mainMenu = false;
        optionsMenu = false;
        float dirPressed = playerControls.Actions.NavigateMenu.ReadValue<Vector2>().x;

        if (i >= PlayerPrefs.GetInt("Level"))
            levelSelectLight.enabled = false;
        else
            levelSelectLight.enabled = true;
        cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[i].transform.position, moveSpeed);
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, camLocations[i].transform.rotation, moveSpeed * 10);
        levelText[i].SetActive(true);
        levelSelectCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);

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
            levelSelectCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            SetSelected("New Game");
        }
    }


    private void HandleOptionMenu()
    {

        mainMenu = false;
        levelSelect = false;
        cam.transform.position = Vector3.Lerp(cam.transform.position, optionsCamPos.transform.position, moveSpeed);
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, optionsCamPos.transform.rotation, moveSpeed * 10);
        optionMenuCanvas.SetActive(true);
    }

    public void SetSelected(string name)
    {
        StartCoroutine(SelectDelay(name));

    }

    IEnumerator SelectDelay(string name)
    {
        yield return new WaitForSeconds(0.1f);
        EventSystem.current.SetSelectedGameObject(GameObject.Find(name));
    }
}
