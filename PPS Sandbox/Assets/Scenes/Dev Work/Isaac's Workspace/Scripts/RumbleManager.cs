using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad controller;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        controller = Gamepad.current;

        if(controller != null)
        {
            controller.SetMotorSpeeds(lowFrequency, highFrequency);
            StartCoroutine(StopRumble(duration, controller));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad controller)
    {
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.SetMotorSpeeds(0f, 0f);
    }
}
