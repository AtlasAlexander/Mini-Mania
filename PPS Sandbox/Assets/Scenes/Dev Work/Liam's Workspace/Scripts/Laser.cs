using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class laser : MonoBehaviour
{
    GameObject CheckpointRef;
    private LineRenderer lr;

    public EventInstance laserSound;
    private int laserVolume;
    void Start()
    {
        laserVolume = 10;
        SetLaserVolume(10);
        CheckpointRef = GameObject.Find("CheckpointController"); 
        lr = GetComponent<LineRenderer>();

        laserSound = FMODUnity.RuntimeManager.CreateInstance(FindObjectOfType<FmodAudioManager>().gameplaySounds[FindObjectOfType<FmodAudioManager>().FindEventReferenceByName("laserConstant")]);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        FMOD.Studio.PLAYBACK_STATE playbackState;
        laserSound.getPlaybackState(out playbackState);

        //if (!playbackState.ToString().Contains("PLAYING"))
        laserSound.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(laserSound, gameObject.transform);
        
        //FindObjectOfType<FmodAudioManager>().QuickPlaySound("laserConstant", gameObject);
    }

    /*  private void OnTriggerEnter(Collider other)
      {
          if (other.CompareTag("Player"))
          {
              Debug.Log("Player killed");
              Destroy(other.gameObject);
          }
      }*/

    // Update is called once per frame



    void Update()
    {

        lr.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("PlayerTrigger"))
            {
                lr.SetPosition(1, hit.point);
                //Kill player
                //Destroy(hit.collider.gameObject);
                Debug.Log("Player has been killed by laser");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                CheckpointRef.GetComponent<CheckpointController>().checkChange = true;
                CheckpointRef.GetComponent<CheckpointController>().LoadCheckpoint();
            }
            else
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }

    public void ToggleLaserSound()
    {
        if(laserVolume == 10)
        {
            laserSound.setVolume(0);
            laserVolume = 0;
        }
        else
        {
            laserSound.setVolume(10);
            laserVolume = 10;
        }
        
    }
    public void SetLaserVolume(int volume)
    {
        laserSound.setVolume(volume);
    }

}
    
