using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private Rigidbody scalePlatforms;

    private Stats stats;
    private Stats stats2;

    private void Awake()
    {
        stats = GameObject.Find("Player").GetComponent<Stats>();
        stats2 = GameObject.Find("Objective Cube").GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stats.Weight >= 100.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && stats.Weight < 100.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Pickup") && stats2.Weight >= 80.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Pickup") && stats2.Weight < 80.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && stats.Weight >= 100.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && stats.Weight < 100.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Pickup") && stats2.Weight >= 80.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Pickup") && stats2.Weight < 80.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.parent = null;                       // Player is not a child of the platform
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }

        if (other.CompareTag("Pickup"))
        {
            player.parent = null;                       // Player is not a child of the platform
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }

    }
}
