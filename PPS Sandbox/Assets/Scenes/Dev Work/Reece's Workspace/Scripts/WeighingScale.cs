using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    // Transforms
    [SerializeField] private Transform cubes;
    [SerializeField] private Transform parentPlatform;

    // Used for putting weight on the platforms
    [SerializeField] private Rigidbody scalePlatforms;
    
    // Weighing scale conditions
    private bool isBigCube;
    private bool isNormalPlayer;
    private bool isSmallPlayer;
    private bool isSmallCube;
    private bool isSwitchingWeights;
    
    // Player & Cube weight values
    private Stats playerWeight;
    private Stats cubeWeight;

    private void Awake()
    {
        playerWeight = GameObject.Find("JakePlayer").GetComponent<Stats>();
        cubeWeight = cubes.GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detects if the player is on the platform by itself
        if (other.CompareTag("Player") && playerWeight.Weight >= 100.0f && !isSwitchingWeights)
        {
            //Debug.Log("Normal size player is on the platform.");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 7.0f;
            // Tracks if the player is on the platform
            isNormalPlayer = true;
        }

        // Detects if the small player is on the platform by itself
        if (other.CompareTag("Player") && playerWeight.Weight < 100.0f && !isSwitchingWeights)
        {
            //Debug.Log("Small size player is on the platform.");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the player is on the platform
            isSmallPlayer = true;
        }

        // Detects if the big cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeWeight.Weight >= 50 && !isSwitchingWeights)
        {
            //Debug.Log("Big cube is on the platform.");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 20.0f;
            // Tracks if the cube is on the platform
            isBigCube = true;
        }

        // Detects if the small cube is on the platform by itself
        if (other.CompareTag("Pickup") && cubeWeight.Weight < 50 && !isSwitchingWeights)
        {
            //Debug.Log("Small cube is on the platform.");

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
            //Debug.Log("Big cube and Normal player are on the platform");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 27.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the big cube and the small player are on the same platform
        if (isBigCube && isSmallPlayer)
        {
            //Debug.Log("Big cube and Small player are on the platform");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 21.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the small cube and the player are on the same platform
        if (isSmallCube && isNormalPlayer)
        {
            //Debug.Log("small cube and player are on the platform");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 8.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the small cube and the small player are on the same platform
        if (isSmallCube && isSmallPlayer)
        {
            //Debug.Log("small cube and small player are on the platform");

            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 2.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        /// USED FOR SWITCHING BETWEEN CUBES WEIGHTS AND PLAYER WEIGHTS
        if (other.CompareTag("Pickup") && cubeWeight.Weight >= 50)
        {
            scalePlatforms.mass = 20.0f;
        }

        if (other.CompareTag("Pickup") && cubeWeight.Weight < 50)
        {
            scalePlatforms.mass = 1.0f;
        }

        if (other.CompareTag("Player") && playerWeight.Weight >= 100.0f)
        {
            scalePlatforms.mass = 7.0f;
        }

        if (other.CompareTag("Player") && playerWeight.Weight < 100.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if (cubeWeight.Weight >= 50 && playerWeight.Weight >= 100 && isSwitchingWeights)
        {
            scalePlatforms.mass = 27.0f;
        }

        if (cubeWeight.Weight < 50 && playerWeight.Weight >= 100 && isSwitchingWeights)
        {
            scalePlatforms.mass = 8.0f;
        }

        if (cubeWeight.Weight < 50 && playerWeight.Weight < 50 && isSwitchingWeights)
        {
            scalePlatforms.mass = 2.0f;
        }

        if (cubeWeight.Weight >= 50 && playerWeight.Weight < 100 && isSwitchingWeights)
        {
            scalePlatforms.mass = 21.0f;
        }
        ///
    }

    private void OnTriggerExit(Collider other)
    {
        // Detects if the player exits platform.
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exits platform.");
            scalePlatforms.mass = 0.1f;
            isNormalPlayer = false;
            isSmallPlayer = false;
            isSwitchingWeights = false;
        }

        // Detects if the cube exits platform.
        if (other.CompareTag("Pickup"))
        {
            scalePlatforms.mass = 0.1f;
            isBigCube = false;
            isSmallCube = false;
            isSwitchingWeights = false;
        }
    }
}
