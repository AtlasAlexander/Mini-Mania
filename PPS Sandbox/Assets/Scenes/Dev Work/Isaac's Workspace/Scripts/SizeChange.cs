using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    [SerializeField] Vector3 smallestSize = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] Vector3 maxSize = new Vector3(1, 1, 1);

    private bool shrunk = false;

    public void ChangeSize(AmmoType ammoType)
    {
        if(ammoType.ToString() == "Shrink")
        {
            ShrinkObject();
            
        }
        if(ammoType.ToString() == "Grow")
        {
            GrowObject();
        }
    }

    void ShrinkObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if(currentSize != smallestSize)
        {
            gameObject.transform.localScale = smallestSize;
            shrunk= true;
        }
    }

    void GrowObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (currentSize != maxSize)
        {
            gameObject.transform.localScale = maxSize;
            shrunk = false;
        }
    }

    public bool GetShrunkStatus()
    {
        return shrunk;
    }
}
