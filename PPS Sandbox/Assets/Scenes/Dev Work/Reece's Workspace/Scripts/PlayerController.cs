using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;                     // CharacterController component
    private Vector3 playerVelocity;                             // player jump/gravity force
    private bool groundedPlayer;                                // Checking if player is on the ground
    private InputManager inputManager;                          // Player input system
    private Transform cameraTransform;                          // Camera transform

    [SerializeField] private float playerSpeed = 2.0f;          // How fast the player will move
    [SerializeField] private float jumpHeight = 1.0f;           // How high the player will jump
    [SerializeField] private float gravityValue = -9.81f;       // The strength of the gravity

    private readonly float distanceFromGround = 0.0f;           // Evaluates how far player is from ground
    private readonly float none = 0.0f;                         // float with nothing in it
    private readonly float inputPress = 0.5f;                   // Evaluating jump input value
    private readonly float force = -3.0f;                       // Evaluates the force of gravity

    private void Start()
    {
        controller = GetComponent<CharacterController>();       // Getting the character controller
        inputManager = InputManager.Instance;                   // An instance of the input system
        cameraTransform = Camera.main.transform;                // Getting the transform of camera
    }

    private void Update()
    {
        IsPlayerGrounded();     // If the player touches the ground enable the jump
        GetPlayerMovement();    // Gets player movement and camera controls
        GetPlayerJump();        // Get player jumping mechanic
        
       // GetChangePlayerScaleBig();      //Get Player Big Cheat
       // GetChangePlayerScaleNormal();   //Get Player Normal Cheat
       // GetChangePlayerScaleSmall();    //Get Plater Small Cheat
    }

    private void IsPlayerGrounded()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < distanceFromGround)
        {
            playerVelocity.y = 0.0f;
        }
    }

    private void GetPlayerMovement()
    {
        Vector2 movement = inputManager.GetMovement();                              // Get the movement from input manager
        Vector3 move = new(movement.x, none, movement.y);                           // Have movement up, down, left, right
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;   // Player will turn with the camera
        move.y = 0.0f;                                                              // Keeps the camera transform in position
        move.Normalize();                                                           // Keeps consistent movement when looking around
        float cameraYRotation = cameraTransform.rotation.eulerAngles.y;             // Get the camera rotation Y-axis
        //transform.rotation = Quaternion.Euler(none, cameraYRotation, none);       // Apply camera Y-axis to rotate player in the Y
        controller.Move(playerSpeed * Time.deltaTime * move);                       // Overall movement functionality
    }

    private void GetPlayerJump()
    {
        float jumpInput = inputManager.GetJumpInput();                              // Calling in jump input from input manager

        // Changes the height position of the player..
        if (jumpInput > inputPress && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * force * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    /*private void GetChangePlayerScaleBig()
    {
        float bigScaleInput = inputManager.GetPlayerScaleBig();

        if(bigScaleInput > inputPress)
        {
            ChangePlayerScale(5);
        }
    }*/

    /*private void GetChangePlayerScaleNormal()
    {
        float normalScaleInput = inputManager.GetPlayerScaleNormal();

        if (normalScaleInput > inputPress)
        {
            ChangePlayerScale(1);
        }
    }*/

    /*private void GetChangePlayerScaleSmall()
    {
        float smallScaleInput = inputManager.GetPlayerScaleSmall();

        if (smallScaleInput > inputPress)
        {
            ChangePlayerScale(0.2f);
        }
    }*/

    /*private void ChangePlayerScale(float playerScale)
    {
        transform.localScale = new Vector3(playerScale, playerScale, playerScale);
    }*/

    public float GetGravityValue()
    {
        return gravityValue;
    }

    public void SetGravityValue(float newGravValue)
    {
        gravityValue= newGravValue;
    }
}
