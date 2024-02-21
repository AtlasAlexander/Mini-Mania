using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    GameObject SaveSystemOBJ;
    public PlayerControls playerControls;
    GameObject player;
    public List<GameObject> Checkpoints;
    GameObject currentCheckpoint;
    int checkp = 0;
    bool checkChange = false;


    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        SaveSystemOBJ = GameObject.Find("SaveSystem");
        player = GameObject.FindGameObjectWithTag("Player");
        currentCheckpoint = Checkpoints[checkp];
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        playerControls.Movement.CheckpointChange.performed += x => manualSet();

        if (PlayerPrefs.GetInt("Checkpoint") == Checkpoints.Count - 1)
        {
            if(SceneManager.GetActiveScene().buildIndex - 1 >= PlayerPrefs.GetInt("Level"))
                SaveSystemOBJ.GetComponent<SaveSystem>().SetLevel();
            PlayerPrefs.SetInt("Checkpoint", 0);
            if(SceneManager.GetActiveScene().buildIndex < 4)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void FixedUpdate()
    {
        if (checkChange)
        {
            checkChange = false;
            LoadCheckpoint();
        }
    }

    void manualSet()
    {
        if (checkp < Checkpoints.Count - 1)
        {
            checkp++;
        }
        else
        {
            checkp = 0;
        }
        currentCheckpoint = Checkpoints[checkp];
        checkChange = true;
    }
    public void SetCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint;
        for (int i = 0; i < Checkpoints.Count; i++)
        {
            if (currentCheckpoint == Checkpoints[i])
            {
                checkp = i;
                SaveSystemOBJ.GetComponent<SaveSystem>().SaveCheckpoint(checkp);
            }
        }
    }

    public GameObject GetCheckpoint()
    {
        return currentCheckpoint;
    }
     
    public void LoadCheckpoint()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }


}
