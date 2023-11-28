using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class SizeChange : MonoBehaviour
{
    [SerializeField] Vector3 smallestSize = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] Vector3 maxSize = new Vector3(1, 1, 1);
    [SerializeField] float changeDuration = 1f;
    
    [SerializeField] bool forceOnSizeChange = false;
    [SerializeField][Range(0f, 3.0f)] float forceMultiplier = 1f;

    [SerializeField] private bool shrunk = false;

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

        if (forceOnSizeChange)
        {                       //This adds some force to objects when they shrink for visual player feedback
            if(GetComponent<Rigidbody>() != null)
            {
                if (currentSize != smallestSize) gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 180f * forceMultiplier);
                else gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 90f * forceMultiplier);
            }
        }

        if (currentSize != smallestSize)
        {
            GetComponent<Stats>().Weight = GetComponent<Stats>().Weight * 0.2f;
            StartCoroutine(LerpSize(currentSize, smallestSize, changeDuration));
            shrunk = true;
            FindObjectOfType<AudioManager>().Play("object_shrink");
        }

        /*
        if(currentSize != smallestSize)
        {
            gameObject.transform.localScale = Vector3.Lerp(currentSize, smallestSize, t * Time.deltaTime);
            
            shrunk= true;
        }
        */
    }

    IEnumerator LerpSize(Vector3 currentSize, Vector3 targetSize, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.localScale = Vector3.Lerp(currentSize, targetSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localScale = targetSize;
    }

    void GrowObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (forceOnSizeChange)
        {           //This adds some force to objects when they grow for visual player feedback
            if(GetComponent<Rigidbody>() != null)
            {
                if (currentSize != maxSize) gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 180f * forceMultiplier);
                else gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 70f * forceMultiplier);
            }
        }

        if (currentSize != maxSize)
        {
            //gameObject.transform.localScale = maxSize;
            GetComponent<Stats>().Weight = GetComponent<Stats>().Weight * 5f;
            StartCoroutine(LerpSize(currentSize, maxSize, changeDuration));
            shrunk = false;
            FindObjectOfType<AudioManager>().Play("object_grow");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        ///only does increase
       // GrowObject();       //Taken out due to switch bug (not sure of use)

        if(other.tag == "SizeOverride")
            GrowObject();
        //{
        //    Vector3 currentSize = GetComponent<Transform>().localScale;
        //
        //    if (currentSize != maxSize)
        //    {
        //        gameObject.transform.localScale = maxSize;
        //        shrunk = false;
        //    }
        //}
    }

    public bool GetShrunkStatus()
    {
        return shrunk;
    }
}
