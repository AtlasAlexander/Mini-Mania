using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private Rigidbody scalePlatforms;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 5.0f;                 // Increase the mass of the scale platform when player interacts
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.parent = null;                       // Player is not a child of the platform
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }
    }
}
