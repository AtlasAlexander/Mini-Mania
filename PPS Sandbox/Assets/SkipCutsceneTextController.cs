using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SkipCutsceneTextController : MonoBehaviour
{
    public TextMeshProUGUI textElement;

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            UpdateText("keyboard");
        }

        // Check if a gamepad is connected and if the south button (ButtonSouth) is pressed
        if (Gamepad.current != null && Gamepad.current.buttonSouth.isPressed)
        {
            UpdateText("gamepad");
        }
    }

    void UpdateText(string inputType = "")
    {
        if (inputType == "keyboard")
        {
            textElement.text = "Press and Hold SPACE to Skip";
        }
        else if (inputType == "gamepad")
        {
            textElement.text = "Press and Hold the South Button on the Gamepad to Skip";
        }
    }
}