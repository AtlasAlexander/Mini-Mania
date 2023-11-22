using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    [SerializeField] Vector3 smallestSize = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] Vector3 maxSize = new Vector3(1, 1, 1);
    [SerializeField] float changeDuration = 1f;

    private bool shrunk = false;

    public void ChangeSize(AmmoType ammoType)
    {
        if(ammoType.ToString() == "Shrink")
        {
            FindObjectOfType<AudioManager>().Play("object_shrink");
            ShrinkObject();
        }
        if(ammoType.ToString() == "Grow")
        {
            FindObjectOfType<AudioManager>().Play("object_grow");
            GrowObject();
        }
    }

    void ShrinkObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (currentSize != smallestSize)
        {
            StartCoroutine(LerpSize(currentSize, smallestSize, changeDuration));
            shrunk = true;
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

        if (currentSize != maxSize)
        {
            //gameObject.transform.localScale = maxSize;
            StartCoroutine(LerpSize(currentSize, maxSize, changeDuration));
            shrunk = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SizeOverride")
        {
            Vector3 currentSize = GetComponent<Transform>().localScale;

            if (currentSize != maxSize)
            {
                gameObject.transform.localScale = maxSize;
                shrunk = false;
            }
        }
    }

    public bool GetShrunkStatus()
    {
        return shrunk;
    }
}
