using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private float fixedScale;
    [SerializeField] private Rigidbody scalePlatforms;

    private FirstPersonController fpsController;

    private void Awake()
    {
        fpsController = GetComponent<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("A Collision has occurred!");
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 5.0f;
            //player.position = parentPlatform.position;
        }
        else
        {
            Debug.Log("No Collision Detected");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("A Collision was exited!");
            player.parent = null;
            scalePlatforms.mass = 0.1f;
        }
    }

    private void Update()
    {
        
    }
}
