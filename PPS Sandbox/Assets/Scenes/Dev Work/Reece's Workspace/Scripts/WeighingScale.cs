using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private Rigidbody scalePlatforms;

    private Stats playerStats;
    private Stats cube1Stats;
    private Stats cube2Stats;

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<Stats>();
        //cube1Stats = GameObject.Find("CarryCube").GetComponent<Stats>();
        //cube2Stats = GameObject.Find("CarryCube (1)").GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerStats.Weight >= 100.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && playerStats.Weight < 100.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Hands") && cube1Stats.Weight >= 50.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Hands") && cube1Stats.Weight < 50.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerStats.Weight >= 100.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && playerStats.Weight < 100.0f)
        {
            player.parent = parentPlatform.transform;
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Hands") && cube1Stats.Weight >= 50.0f)
        {
            player.parent = parentPlatform.transform;   // Player becomes the child of the platform
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Hands") && cube1Stats.Weight < 50.0f)
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

        if (other.CompareTag("Hands"))
        {
            player.parent = null;                       // Player is not a child of the platform
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }

    }
}
