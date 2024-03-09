using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("REFERENCES")]
    public GameObject[] MainMenuOptions;
    public GameObject[] camLocations;
    public GameObject[] levelText;
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

    private void Awake()
    {
        playerControls = new PlayerControls();
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
        mainCamPos = GameObject.Find("MenuCamPos").transform;
        levelSelectLight = cam.GetComponent<Light>();
        levelSelectLight.enabled = false;
    }

    private void Update()
    {
        float moveSpeed = Time.deltaTime * speed;
        if (playerControls.Actions.Pause.IsPressed() && !startPressed)
        {
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
            levelSelectLight.enabled = true;
            cam.transform.position = Vector3.Lerp(cam.transform.position, camLocations[i].transform.position, moveSpeed);
            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, camLocations[i].transform.rotation, moveSpeed * 10);
            levelText[i].SetActive(true);
            if (playerControls.Actions.NavigateMenu.WasPressedThisFrame() && dirPressed > 0)
            {
                i += 1;
                levelText[i - 1].SetActive(false);
            }

            if (playerControls.Actions.NavigateMenu.WasPressedThisFrame() && dirPressed < 0)
            {
                i -= 1;
                levelText[i + 1].SetActive(false);
            }

            if (i >= camLocations.Length)
            {
                i = 0;
            }

            if (i < 0)
            {
                i = camLocations.Length - 1;
            }

            if (playerControls.Actions.Pause.IsPressed())
            {
                levelSelect = !levelSelect;
                levelText[i].SetActive(false);
                levelSelectLight.enabled = false;
            }
        }
    }
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Load Current Saved Level
        FindObjectOfType<FmodAudioManager>().killMusic();
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + 1);
        
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
        levelSelect = !levelSelect;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("gg");
    }
}
