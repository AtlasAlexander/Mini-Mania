using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float changeAmount;
    [SerializeField] Vector3 smallestSize;
    [SerializeField] Vector3 maxSize;
    private bool inRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }

    public void MirrorSizeChange(AmmoType ammoType)
    {
        if (ammoType.ToString() == "Shrink" && inRange)
        {
            ShrinkPlayer();

        }
        if (ammoType.ToString() == "Grow" && inRange)
        {
            GrowPlayer();
        }

        
    }

    void ShrinkPlayer()
    {

        //Debug.Log("working shrink " + changeAmount);
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (currentSize != smallestSize)
        {


            Vector3 newSize = player.transform.localScale -= new Vector3(changeAmount, changeAmount, changeAmount);

            if (newSize != smallestSize)
            {
                player.transform.localScale = newSize;
            }
            else
            {
                return;
            }
        }
    }

    void GrowPlayer()
    {
        //Debug.Log("working increase " + changeAmount);
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (currentSize != maxSize)
        {
            Vector3 newSize = player.transform.localScale += new Vector3(changeAmount, changeAmount, changeAmount);

            if (newSize != maxSize)
            {
                player.transform.localScale = newSize;
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

        //Debug.Log(inRange);
    }

    void CheckSmallest()
    {
        if (player.transform.localScale.x < smallestSize.x
            && player.transform.localScale.y < smallestSize.y
            && player.transform.localScale.z < smallestSize.z)
        {
            player.transform.localScale = smallestSize;
            //Debug.Log("too small");
        }
    }

    void CheckLargest()
    {
        if (player.transform.localScale.x > maxSize.x
            && player.transform.localScale.y > maxSize.y
            && player.transform.localScale.z > maxSize.z)
        {
            player.transform.localScale = maxSize;
            //Debug.Log("too big");
        }
    }
}
