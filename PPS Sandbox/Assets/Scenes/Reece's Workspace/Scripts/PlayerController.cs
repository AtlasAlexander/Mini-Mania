using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private InputSystem inputSystem = null;         // Unity's new input system
    private Vector2 moveVector = Vector2.zero;      // The players direction
    private Rigidbody rb = null;                    // Used for physics of movement
    [SerializeField] private float moveSpeed;       // How fast the player will move

    // Start is called before the first frame update
    private void Awake()
    {
        // An instance of Unity's new input system
        inputSystem = new InputSystem();

        rb = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Movement.performed += OnMovementPerformed;   // Enable when an input is performed
        inputSystem.Player.Movement.canceled += OnMovementCanceled;     // Enable when there is no input
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Player.Movement.performed -= OnMovementPerformed;   // Disable when there is no input performed
        inputSystem.Player.Movement.canceled -= OnMovementCanceled;     // Diable when an input has been performed
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Calculated movement every frame using velocity
        rb.velocity = new Vector3((moveVector.x * moveSpeed) * Time.fixedDeltaTime, 0.0f, 
            (moveVector.y * moveSpeed) * Time.fixedDeltaTime);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();    // Get the value of input vector
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;  // vector goes to zero when there is no input
    }
}
