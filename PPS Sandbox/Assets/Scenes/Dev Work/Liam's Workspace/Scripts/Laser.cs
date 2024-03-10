using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class laser : MonoBehaviour
{
    GameObject CheckpointRef;
    private LineRenderer lr;

    public EventInstance laserSound;
    void Start()
    {
        laserSound = FMODUnity.RuntimeManager.CreateInstance(FindObjectOfType<FmodAudioManager>().gameplaySounds[FindObjectOfType<FmodAudioManager>().FindEventReferenceByName("laserConstant")]);
        CheckpointRef = GameObject.Find("CheckpointController"); 
        lr = GetComponent<LineRenderer>();
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
        FMOD.Studio.PLAYBACK_STATE playbackState;
        laserSound.getPlaybackState(out playbackState);
        if (!playbackState.ToString().Contains("PLAYING"))
        {
            laserSound.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(laserSound, gameObject.transform);
        }

        lr.SetPosition(0, transform.position);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("PlayerTrigger"))
            {
                lr.SetPosition(1, hit.point);
                //Kill player
                //Destroy(hit.collider.gameObject);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //CheckpointRef.GetComponent<CheckpointController>().LoadCheckpoint();
            }
            else
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 5000);
    }
}
    
