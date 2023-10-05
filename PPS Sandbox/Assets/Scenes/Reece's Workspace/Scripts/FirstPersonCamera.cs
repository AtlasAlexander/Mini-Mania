using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    private InputSystem inputSystem = null;             // Unity's new input system
    private Vector2 camVector = Vector2.zero;           // Camera direction
    private float camVerticalRot = 0.0f;                // Vertical camera rotation
    [SerializeField] private float mouseSensitivity;    // The speed the camera rotates
    private float camClamp = 90.0f;                     // Clamps the vertical rotation
    private Transform player;           // Using the transform to rotate the camera & player horizontally

    private void Awake()
    {   
        // Getting an instance of the input system
        inputSystem = new InputSystem();

        // Finding the parent of the camera
        player = gameObject.transform.parent;

        // Lock and hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Look.performed += OnLookPerformed;   // Enabled when the input is performed
        inputSystem.Player.Look.canceled += OnLookCanceled;     // Enabled when input has been released
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Player.Look.performed -= OnLookPerformed;   // Disabled when there is no input performed
        inputSystem.Player.Look.canceled -= OnLookCanceled;     // Disabled when an input has been performed
    }

    private void FixedUpdate()
    {
        // Camera rotations are performed each frame
        VerticalLookRotation();
        HorizontalLookRotation();
        Debug.Log(camVector);
    }

    private void OnLookPerformed(InputAction.CallbackContext value)
    {
        camVector = value.ReadValue<Vector2>();     // Get the value of input vector
    }

    private void OnLookCanceled(InputAction.CallbackContext value)
    {
        camVector = Vector2.zero;   // vector goes to zero when there is no input
    }

    private Vector3 VerticalLookRotation()
    {
        // Cameras vertical look rotation is clamped 90 degrees eachh side
        camVerticalRot -= camVector.y;
        camVerticalRot = Mathf.Clamp(camVerticalRot, -camClamp, camClamp);

        // returns the direction vertical rotation with mouse sensitivity to produce the movement 
        return transform.localEulerAngles = (camVerticalRot * Time.fixedDeltaTime * Vector2.right) * mouseSensitivity;
    }

    private void HorizontalLookRotation()
    {
        // Rotates the transform of player with the child of the camera
        player.Rotate((camVector.x * Time.fixedDeltaTime * Vector3.up) * mouseSensitivity);
    }
}
