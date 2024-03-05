using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Weight = 50;
    public float DefaultWeight = 50;
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
