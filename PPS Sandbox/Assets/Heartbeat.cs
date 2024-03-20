using UnityEngine;
using UnityEngine.ProBuilder;

public class Heartbeat : MonoBehaviour
{
    public float beatDuration = 1.0f; // Duration of one heartbeat cycle
    public float scaleMultiplier = 1.5f; // Scale multiplier for the heartbeat effect
    public float pauseDuration = 0.5f; // Pause duration between heartbeats

    private Vector3 originalScale;
   
    private bool isExpanding = false;
    private bool paused = false;
    private float timer = 0.0f;

    private GameObject speaker1;
    private GameObject speaker2;

    void Start()
    {
        
        Transform findSpeaker1 = transform.Find("body/speaker1");
        speaker1 = findSpeaker1.gameObject;
        
        Transform findSpeaker2 = transform.Find("body/speaker2");
        speaker2 = findSpeaker2.gameObject;
        originalScale = speaker1.transform.localScale;
        
    }

    void Update()
    {
        if (gameObject.GetComponent<FmodMusicManager>().paused)
        {
            timer = 0;
            speaker1.transform.localScale = originalScale;
            speaker2.transform.localScale = originalScale;
        }
        else{
           
            timer += Time.deltaTime;

            
            if (!paused && timer >= beatDuration)
            {
               
                isExpanding = !isExpanding;
                if (isExpanding)
                {
                    paused = true;
                }

                timer = 0.0f; 
            }
            if (paused && timer > pauseDuration)
            {
                paused = false;
                timer = 0.0f;
            }

           
            if (!paused && isExpanding)
            {




                float scaleFactorX = Mathf.Lerp(originalScale.x, originalScale.x * scaleMultiplier, timer / beatDuration);
                float scaleFactorY = Mathf.Lerp(originalScale.y, originalScale.y * scaleMultiplier, timer / beatDuration);
                float scaleFactorZ = Mathf.Lerp(originalScale.z, originalScale.z * scaleMultiplier, timer / beatDuration);
                speaker1.transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);
                speaker2.transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);
            }
            else if (!paused && !isExpanding)
            {
                float scaleFactorX = Mathf.Lerp(originalScale.x * scaleMultiplier, originalScale.x, timer / beatDuration);
                float scaleFactorY = Mathf.Lerp(originalScale.y * scaleMultiplier, originalScale.y, timer / beatDuration);
                float scaleFactorZ = Mathf.Lerp(originalScale.z * scaleMultiplier, originalScale.z, timer / beatDuration);
                speaker1.transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);
                speaker2.transform.localScale = new Vector3(scaleFactorX, scaleFactorY, scaleFactorZ);
            }
        }
        
    }
}