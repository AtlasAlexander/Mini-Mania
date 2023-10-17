using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem inputSystem;

    private static InputManager instance;

    public static InputManager Instance
    {
        get 
        { 
            return instance; 
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        // Creating an instance of the input manager class
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        inputSystem = new InputSystem();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    public Vector2 GetMovement()
    {
        // Getting value vector2 for movement
        return inputSystem.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        // Getting value vector2 for the camera
        return inputSystem.Player.Look.ReadValue<Vector2>();
    }

}
