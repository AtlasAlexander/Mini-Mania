using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Weight = 50;
    public float DefaultWeight;

    private void Awake()
    {
        DefaultWeight = Weight;
    }
    private void LateUpdate()
    {
        if(Weight < DefaultWeight/5)
        {
            Weight = DefaultWeight / 5;
        }
        if(Weight > DefaultWeight)
        {
            Weight = DefaultWeight;
        }
    }
}
