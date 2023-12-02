using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Weight = 100;
    float DefaultWeight = 100;

    private void Awake()
    {
        DefaultWeight = Weight;
        if(GetComponent<SizeChange>().startSmall)
        {
            DefaultWeight = Weight * 5;
        }
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
