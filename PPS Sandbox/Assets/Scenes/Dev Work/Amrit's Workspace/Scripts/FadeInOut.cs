using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [HideInInspector] public bool fadeIn = false;
    [HideInInspector] public bool fadeOut = false;

    [HideInInspector] public float timeToFade;

    public CanvasGroup canvasGroup;

    private void Update()
    {
        if (fadeIn == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;

                if (canvasGroup.alpha == 0)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeToFade * Time.deltaTime;

                if (canvasGroup.alpha >= 1)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public IEnumerator FadeIn(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        fadeIn = true;
    } 
    
    public IEnumerator FadeOut(float timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);
        fadeOut = true;
    }
}
