using UnityEngine;

public class WeighingScale : MonoBehaviour
{
    // Transforms
    [SerializeField] private Transform cube1;
    [SerializeField] private Transform cube2;
    [SerializeField] private Transform parentPlatform;

    // Used for putting weight on the platforms
    [SerializeField] private Rigidbody scalePlatforms;
    
    // Weighing scale conditions
    private bool isBigCube;
    private bool isBigCube2;
    private bool isNormalPlayer;
    private bool isSmallPlayer;
    private bool isSmallCube;
    private bool isSmallCube2;
    private bool isSwitchingWeights;
    private bool hasMultipleWeights;
    
    // Player & Cube weight values
    private Stats playerWeight;
    private Stats cube1Weight;
    private Stats cube2Weight;

    private string carryCube1 = "CarryCube Big";
    private string carryCube2 = "CarryCube Big (1)";

    private void Awake()
    {
        playerWeight = GameObject.Find("Player").GetComponent<Stats>();
        cube1Weight = cube1.GetComponent<Stats>();
        cube2Weight = cube2.GetComponent<Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detects if the player is on the platform by itself
        if (other.CompareTag("Player") && playerWeight.Weight >= 100.0f && !isSwitchingWeights)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 7.0f;
            // Tracks if the player is on the platform
            isNormalPlayer = true;
        }

        // Detects if the small player is on the platform by itself
        if (other.CompareTag("Player") && playerWeight.Weight < 100.0f && !isSwitchingWeights)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the player is on the platform
            isSmallPlayer = true;
        }

        // Detects if the big cube is on the platform by itself
        if (other.name == carryCube1 && cube1Weight.Weight >= 50 && !isSwitchingWeights)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 20.0f;
            // Tracks if the cube is on the platform
            isBigCube = true;
        }

        // Detects if the small cube is on the platform by itself
        if (other.name == carryCube1 && cube1Weight.Weight < 50 && !isSwitchingWeights)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 1.0f;
            // Tracks if the cube is on the platform
            isSmallCube = true;
        }

        // Detects if the big cube is on the platform by itself
        if (other.name == carryCube2 && cube2Weight.Weight >= 50)
        {
            // Tracks if the cube is on the platform
            isBigCube2 = true;
        }

        if (other.name == carryCube2 && cube2Weight.Weight < 50)
        {
            // Tracks if the cube is on the platform
            isSmallCube2 = true;
        }

    }

    private void CarryMultipleCubes(Collider other)
    {
        if (other.name == carryCube2 && cube2Weight.Weight >= 50)
        {
            //Debug.Log("Big cube 2");
            scalePlatforms.mass = 20.0f;
            isBigCube2 = true;
        }

        if (other.name == carryCube2 && cube2Weight.Weight < 50)
        {
            //Debug.Log("Small cube 2");
            scalePlatforms.mass = 1.0f;
            isSmallCube2 = true;
        }

        if ((isBigCube && cube1Weight.Weight >= 50) && (isBigCube2 && cube2Weight.Weight >= 50))
        {
            Debug.Log("Big cube 1 & Big cube 2");
            scalePlatforms.mass = 40.0f;
        }

        if ((isBigCube2 && cube2Weight.Weight >= 50) && (isBigCube && cube1Weight.Weight >= 50))
        {
            //Debug.Log("2");
            scalePlatforms.mass = 40.0f;
        }

        if ((isBigCube && cube1Weight.Weight >= 50) && (isSmallCube2 && cube2Weight.Weight < 50))
        {
            //Debug.Log("1");
            scalePlatforms.mass = 21.0f;
        }

        if ((isSmallCube2 && cube2Weight.Weight < 50) && (isBigCube && cube1Weight.Weight >= 50))
        {
            //Debug.Log("Small cube 2 & Big cube 1");
            scalePlatforms.mass = 21.0f;
        }

        if ((isBigCube2 && cube2Weight.Weight >= 50) && (isSmallCube && cube1Weight.Weight < 50))
        {
            //Debug.Log("1");
            scalePlatforms.mass = 21.0f;
        }

        if ((isSmallCube && cube1Weight.Weight < 50) && (isBigCube2 && cube2Weight.Weight >= 50))
        {
            //Debug.Log("Small cube 1 & Big cube 2");
            scalePlatforms.mass = 21.0f;
        }

        if ((isSmallCube && cube1Weight.Weight < 50) && (isSmallCube2 && cube2Weight.Weight < 50))
        {
            //Debug.Log("1");
            scalePlatforms.mass = 2.0f;
        }

        if ((isSmallCube2 && cube2Weight.Weight < 50) && (isSmallCube && cube1Weight.Weight < 50))
        {
            //Debug.Log("1");
            scalePlatforms.mass = 2.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        CarryMultipleCubes(other);

        // Detects if the big cube and the player are on the same platform
        if (isBigCube && isNormalPlayer)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 27.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the big cube and the small player are on the same platform
        if (isBigCube && isSmallPlayer)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 21.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the small cube and the player are on the same platform
        if (isSmallCube && isNormalPlayer)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 8.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        // Detects if the small cube and the small player are on the same platform
        if (isSmallCube && isSmallPlayer)
        {
            // Change the mass of the scale platform when player interacts
            scalePlatforms.mass = 2.0f;
            // Tracks if the cube is on the platform
            isSwitchingWeights = true;
        }

        /// USED FOR SWITCHING BETWEEN CUBES WEIGHTS AND PLAYER WEIGHTS

        if ((other.name == "CarryCube Big" && cube1Weight.Weight >= 50))
        {
            scalePlatforms.mass = 20.0f;
            Debug.Log("3");
            isBigCube = true;
        }

        if (other.name == "CarryCube Big" && cube1Weight.Weight < 50)
        {
            scalePlatforms.mass = 1.0f;
            isSmallCube = true;
        }

        if (other.CompareTag("Player") && playerWeight.Weight >= 100.0f)
        {
            scalePlatforms.mass = 7.0f;
        }

        if (other.CompareTag("Player") && playerWeight.Weight < 100.0f)
        {
            scalePlatforms.mass = 1.0f;
        }

        if (cube1Weight.Weight >= 50 && playerWeight.Weight >= 100 && isSwitchingWeights)
        {
            scalePlatforms.mass = 27.0f;
        }

        if (cube1Weight.Weight < 50 && playerWeight.Weight >= 100 && isSwitchingWeights)
        {
            scalePlatforms.mass = 8.0f;
        }

        if (cube1Weight.Weight < 50 && playerWeight.Weight < 50 && isSwitchingWeights)
        {
            scalePlatforms.mass = 2.0f;
        }

        if (cube1Weight.Weight >= 50 && playerWeight.Weight < 100 && isSwitchingWeights)
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
        if (other.name == "CarryCube Big")
        {
            //Debug.Log("Carry Cube 1");
            scalePlatforms.mass = 0.1f;
            isBigCube = false;
            isSmallCube = false;
            isSwitchingWeights = false;
        }

        if (other.name == "CarryCube Big (1)")
        {
            //Debug.Log("Carry Cube 2");
            scalePlatforms.mass = 0.1f;
            isBigCube2 = false;
            isSmallCube2 = false;
            isSwitchingWeights = false;
        }
    }
}
