using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    [SerializeField] Vector3 smallestSize;
    [SerializeField] Vector3 maxSize;

    public void ChangeSize(AmmoType ammoType, float changeAmount)
    {
        if(ammoType.ToString() == "Shrink")
        {
            ShrinkObject(changeAmount);
            
        }
        if(ammoType.ToString() == "Increase")
        {
            IncreaseObject(changeAmount);
        }
    }

    void ShrinkObject(float changeAmount)
    {
        //Debug.Log("working shrink " + changeAmount);
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if(currentSize != smallestSize)
        {


            Vector3 newSize = gameObject.transform.localScale -= new Vector3(changeAmount, changeAmount, changeAmount);

            if(newSize != smallestSize) 
            {
                gameObject.transform.localScale = newSize;
            }
            else
            {
                return;
            }         
        }
    }

    void IncreaseObject(float changeAmount)
    {
        //Debug.Log("working increase " + changeAmount);
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (currentSize != maxSize)
        {
            Vector3 newSize = gameObject.transform.localScale += new Vector3(changeAmount, changeAmount, changeAmount);

            if (newSize != maxSize)
            {
                gameObject.transform.localScale = newSize;
            }
            else
            {
                return;
            }
        }
    }

    private void Update()
    {
        CheckSmallest();
        CheckLargest();
    }

    void CheckSmallest()
    {
        if (gameObject.transform.localScale.x < smallestSize.x
            && gameObject.transform.localScale.y < smallestSize.y
            && gameObject.transform.localScale.z < smallestSize.z)
        {
            gameObject.transform.localScale = smallestSize;
            //Debug.Log("too small");
        }
    }

    void CheckLargest()
    {
        if (gameObject.transform.localScale.x > maxSize.x
            && gameObject.transform.localScale.y > maxSize.y
            && gameObject.transform.localScale.z > maxSize.z)
        {
            gameObject.transform.localScale = maxSize;
            //Debug.Log("too big");
        }
    }
}
