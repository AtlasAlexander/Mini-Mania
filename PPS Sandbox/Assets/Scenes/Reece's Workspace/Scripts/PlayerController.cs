using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;                     // CharacterController component
    private Vector3 playerVelocity;                             // player jump/gravity force
    //private bool groundedPlayer;                              // Checking if player is on the ground
    private InputManager inputManager;                          // Player input system
    //private Oxygen oxygen;
    private Transform cameraTransform;                          // Camera transform

    [SerializeField] private float playerSpeed = 2.0f;          // How fast the player will move
    //[SerializeField] private float jumpHeight = 1.0f;         // How high the player will jump
    [SerializeField] private float gravityValue = -9.81f;       // The strength of the gravity

    private void Start()
    {
        controller = GetComponent<CharacterController>();       // Getting the character controller
        inputManager = InputManager.Instance;                   // An instance of the input system
        cameraTransform = Camera.main.transform;                // Getting the transform of camera
        //oxygen = FindObjectOfType<Oxygen>();
    }

    private void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        Vector2 movement = inputManager.GetMovement();                              // Get the movement from input manager
        Vector3 move = new Vector3(movement.x, 0.0f, movement.y);                   // Have movement up, down, left, right
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;   // Player will turn with the camera
        controller.Move(playerSpeed * Time.deltaTime * move);                       // Overall movement functionality

        // Changes the height position of the player..
        /*if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }*/

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        
        //oxygen.ReplenishOxygen(10);
        
    }
}
