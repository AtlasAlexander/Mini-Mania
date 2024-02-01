using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cubes;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private Rigidbody scalePlatforms;
    private float totalWeight;
    private Stats playerStats;
    private Stats cubeStats;

    private void Awake()
    {
        playerStats = GameObject.Find("JakePlayer").GetComponent<Stats>();
        cubeStats = FindAnyObjectByType<Stats>();

        totalWeight = playerStats.Weight + cubeStats.Weight; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerStats.Weight >= 100.0f)
        {
            scalePlatforms.mass = 7.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && playerStats.Weight < 100.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Pickup") && cubeStats.Weight >= 50.0f)
        {
            scalePlatforms.mass = 20.0f;                // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Pickup") && cubeStats.Weight < 50.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if ((other.CompareTag("Pickup")) && (other.CompareTag("Player")) && (totalWeight >= 150))
        {
            scalePlatforms.mass = 20.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerStats.Weight >= 100.0f)
        {
            scalePlatforms.mass = 7.0f;                // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Player") && playerStats.Weight < 100.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Pickup") && cubeStats.Weight >= 50.0f)
        {
            scalePlatforms.mass = 20.0f;                 // Increase the mass of the scale platform when player interacts
        }
        else if (other.CompareTag("Pickup") && cubeStats.Weight < 50.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if ((other.CompareTag("Player") && playerStats.Weight >= 100.0f) && (other.CompareTag("Pickup") && cubeStats.Weight >= 100.0f))
        {
            Debug.Log("Both objects on the platform");
            scalePlatforms.mass = 20.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }

        if (other.CompareTag("Pickup"))
        {
            scalePlatforms.mass = 0.1f;                 // Decrease the mass of scale platform when player comes off
        }

      /*  if ((other.CompareTag("Player")) || (other.CompareTag("Pickup")))
        {
            scalePlatforms.mass = 0.1f;
        }*/

    }
}
