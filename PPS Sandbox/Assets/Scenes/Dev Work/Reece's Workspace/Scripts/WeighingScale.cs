using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cubes;
    [SerializeField] private Transform parentPlatform;
    [SerializeField] private Rigidbody scalePlatforms;
    [SerializeField] private float weight;
    [SerializeField] private bool isBigCube;
    [SerializeField] private bool isNormalPlayer;
    [SerializeField] private bool isSmallPlayer;
    [SerializeField] private bool isSmallCube;
    private float totalWeight;
    private Stats playerStats;
    private Stats cubeStats;

    private void Awake()
    {
        playerStats = GameObject.Find("JakePlayer").GetComponent<Stats>();
        cubeStats = cubes.GetComponent<Stats>();

        totalWeight = playerStats.Weight + cubeStats.Weight;
        Debug.Log(totalWeight);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detects if the player is on the platform by itself
        if (other.CompareTag("Player") && playerStats.Weight >= 100.0f)
        {
            Debug.Log("Normal size player is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 7.0f;
            // Tracks if the player is on the platform
            isNormalPlayer = true;
        }

        // Detects if the small player is on the platform by itself
        if (other.CompareTag("Player") && playerStats.Weight < 100.0f)
        {
            Debug.Log("Small size player is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the player is on the platform
            isSmallPlayer = true;
        }

        // Detects if the big cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeStats.Weight >= 50)
        {
            Debug.Log("Big cube is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 20.0f;
            // Tracks if the cube is on the platform
            isBigCube = true;
        }

        // Detects if the small cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeStats.Weight < 50)
        {
            Debug.Log("Small cube is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the cube is on the platform
            isSmallCube = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Detects if the big cube and the player are on the same platform
        if (isBigCube && isNormalPlayer)
        {
            Debug.Log("Big cube and Normal player are on the platform");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 27.0f;
        }

        // Detects if the big cube and the small player are on the same platform
        if (isBigCube && isSmallPlayer)
        {
            Debug.Log("Big cube and Small player are on the platform");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 21.0f;
        }

        // Detects if the small cube and the player are on the same platform
        if (isSmallCube && isNormalPlayer)
        {
            Debug.Log("small cube and player are on the platform");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 8.0f;
        }

        // Detects if the small cube and the small player are on the same platform
        if (isSmallCube && isSmallPlayer)
        {
            Debug.Log("small cube and small player are on the platform");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 2.0f;
        }

        /// NEED TO FIX THE CUBE WEIGHT SWITCHING ON PLATFORM!
        /*if (!isSmallCube && isBigCube)
        {
            Debug.Log("Big cube is now small cube");
        }

        // Detects if the big cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeStats.Weight >= 50)
        {
            Debug.Log("Big cube is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 20.0f;
            // Tracks if the cube is on the platform
            //isBigCube = true;
        }

        // Detects if the small cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeStats.Weight < 50)
        {
            Debug.Log("Small cube is on the platform.");
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the cube is on the platform
            //isSmallCube = true;
        }*/
        ///
    }

    private void OnTriggerExit(Collider other)
    {
        /// MORE CONDITIONS NEED TO BE COMPLETED!

        // Detects if the big cube exits platform.
        if (other.CompareTag("Pickup") && isBigCube)
        {
            Debug.Log("Big cube exits platform.");
            scalePlatforms.mass = 0.1f;
            isBigCube = false;
        }

        if (other.CompareTag("Pickup") && !isBigCube && isNormalPlayer)
        {
            Debug.Log("Big cube exits platform.");
            scalePlatforms.mass = 7.0f;
            isBigCube = false;
        }


        // Detects if the small cube exits platform.
        if (other.CompareTag("Pickup") && isSmallCube && !isBigCube)
        {
            Debug.Log("Small cube exits platform.");
            isSmallCube = false;
        }

        // Detects if the player exits platform.
        if (other.CompareTag("Player") && isNormalPlayer && !isSmallPlayer)
        {
            Debug.Log("Player exits platform.");
            isNormalPlayer = false;
        }

        // Detects if the small player exits platform.
        if (other.CompareTag("Player") && !isNormalPlayer && isSmallPlayer)
        {
            Debug.Log("Small player exits platform.");
            isSmallPlayer = false;
        }
    }
}
