using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;
    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool InteractInput { get; private set; }
    public bool ShootInput { get; private set; }
    public bool ZoomInput { get; private set; }
    public bool ZoomInputReleased { get; private set; }

    private PlayerInput playerInput;

    private InputAction move;
    private InputAction jump;
    private InputAction interact;
    private InputAction shoot;
    private InputAction zoom;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    private void Update()
    {
        UpdateInputs();
    }
    void SetupInputActions()
    {
/*        move = playerInput.Player.["Move"];
        jump = playerInput.actions["Jump"];
        interact = playerInput.actions["Interact"];
        shoot = playerInput.actions["Shoot"];
        zoom = playerInput.actions["Zoom"];*/
    }
    void UpdateInputs()
    {
        MoveInput = move.ReadValue<Vector2>();
        JumpInput = jump.WasPressedThisFrame();
        InteractInput = interact.WasPressedThisFrame();
        ShootInput = shoot.WasPressedThisFrame();
        ZoomInput = zoom.WasPressedThisFrame();
        ZoomInputReleased = zoom.WasReleasedThisFrame();
    }
}
