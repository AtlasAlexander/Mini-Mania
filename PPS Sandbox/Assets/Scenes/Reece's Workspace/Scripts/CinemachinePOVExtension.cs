using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float horizontalSpeed = 1.0f;      // Camera horizontal rotation speed
    [SerializeField] private float verticalSpeed = 1.0f;        // Camera vertical rotation speed
    [SerializeField] private float clampAngle = 80.0f;          // Camera clamp in vertical motion

    private InputManager inputManager;                          
    private Vector3 startingRotation;                           // Used to rotate the camera locally

    protected override void Awake()
    {
        // Instance of input manager
        inputManager = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;

                    // Get the mouse delta from input manager
                    Vector2 deltaInput = inputManager.GetMouseDelta();

                    // Rotation functionality in the X and Y axis
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;

                    // Clamping the vertical camera angle
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                    // The overall orientation of the first person camera
                    state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0.0f);
                }
            }
        }
    }
}
