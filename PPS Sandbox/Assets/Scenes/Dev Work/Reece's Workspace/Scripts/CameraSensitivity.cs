using UnityEngine;
using Cinemachine;

public class CameraSensitivity : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;          // Get virtual cam component
    [SerializeField] private float sensitivity = 200.0f;    // Look sensitivity

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Get the CinemachinePOV component
            CinemachinePOV povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();

            if (povComponent != null)
            {
                // Adjust the horizontal and vertical sensitivities
                povComponent.m_HorizontalAxis.m_MaxSpeed = sensitivity;
                povComponent.m_VerticalAxis.m_MaxSpeed = sensitivity;
            }
        }
    }
}
