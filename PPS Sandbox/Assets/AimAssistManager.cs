using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssistManager : MonoBehaviour
{
    public FirstPersonController fpc;
    public float originalSensX;
    public float originalSensY;
    public bool AssistOn;
    public float Timer, test;
    void Start()
    {
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        originalSensX = fpc.mouseLookSpeedX;
        originalSensY = fpc.mouseLookSpeedY;
    }

    public void TestFunc()
    {
        fpc.mouseLookSpeedX = originalSensX * 0.5f;
        fpc.mouseLookSpeedY = originalSensY * 0.5f;
        AssistOn = true;
    }

    private void Update()
    {
        test = fpc.mouseLookSpeedY;
        if(AssistOn)
        {
            Timer += Time.deltaTime;
        }
        if(Timer >= 0.3)
        {
            AssistOn = false;
            Timer = 0;
        }
        if(!AssistOn)
        {
            fpc.mouseLookSpeedX = originalSensX;
            fpc.mouseLookSpeedY = originalSensY;
        }
    }
}
