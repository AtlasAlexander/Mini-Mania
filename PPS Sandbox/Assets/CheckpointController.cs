using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointController : MonoBehaviour
{
    public PlayerControls playerControls;
    GameObject player;
    [SerializeField] List<GameObject> Checkpoints;
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
        player = GameObject.FindGameObjectWithTag("Player");
        currentCheckpoint = Checkpoints[checkp];
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        playerControls.Movement.CheckpointChange.performed += x => manualSet();

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
