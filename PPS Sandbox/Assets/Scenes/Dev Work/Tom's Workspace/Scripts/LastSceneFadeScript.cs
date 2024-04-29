using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneFadeScript : MonoBehaviour
{
    [SerializeField] GameObject grandpa;

    [SerializeField] FmodAudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioManager.QuickPlaySound("shootGrowthRay", grandpa);
    }

    public void Transition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
