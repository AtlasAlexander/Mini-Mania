using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class SizeChange : MonoBehaviour
{
    [SerializeField] Vector3 smallestSize = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] Vector3 maxSize = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] float changeDuration = 1f;

    [SerializeField] bool forceOnSizeChange = false;
    [SerializeField][Range(0f, 2f)] float forceMultiplier;

    [SerializeField] private bool shrunk = false;
    [SerializeField] private bool canBePickedUp = true;

    public bool startSmall;
    public bool isChangingSize;

    private GameObject player;
    NewGrabbing grabbing;


    public void Awake()
    {
        player = GameObject.Find("Player");
        grabbing = player.GetComponent<NewGrabbing>();

        if (startSmall)
        {
            //ShrinkObject();
            //gameObject.transform.localScale = smallestSize;
            //shrunk = true;
            // GetComponent<Stats>().Weight = 50;
            //ShrinkObject();
            Vector3 currentSize = GetComponent<Transform>().localScale;

            GetComponent<Stats>().Weight = GetComponent<Stats>().Weight * 0.2f;
            StartCoroutine(LerpSize(currentSize, smallestSize, 0.1f));
            shrunk = true;
        }
    }
    public void ChangeSize(AmmoType ammoType)
    {
        if (!grabbing.grab)
        {
            if (ammoType.ToString() == "Shrink")
            {
                ShrinkObject();

                if (gameObject.tag == "Player")
                { FindObjectOfType<FmodAudioManager>().SetFootstepsRate(0.2f); }
            }
            if (ammoType.ToString() == "Grow")
            {
                GrowObject();

                if (gameObject.tag == "Player")
                { FindObjectOfType<FmodAudioManager>().SetFootstepsRate(0.4f); }
            }
        }

    }

    void ShrinkObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (forceOnSizeChange)
        {                       //This adds some force to objects when they shrink for visual player feedback
            if (GetComponent<Rigidbody>() != null)
            {
                Vector3 upForce = new Vector3(Random.Range(-0.90f, 0.90f), 1.0f, Random.Range(-0.90f, 0.90f));
                if (currentSize != smallestSize)
                {
                    gameObject.GetComponent<Rigidbody>().AddRelativeForce(upForce * 7f * forceMultiplier);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().AddRelativeForce(upForce * 10f * forceMultiplier);
                }
            }
        }

        if (currentSize != smallestSize)
        {
            GetComponent<Stats>().Weight = GetComponent<Stats>().Weight * 0.2f;
            StartCoroutine(LerpSize(currentSize, smallestSize, changeDuration));
            shrunk = true;
            //FindObjectOfType<AudioManager>().Play("object_shrink");
            if (gameObject.tag == "Player")
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("playerShrink", gameObject);
            }
            else
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("objectShrink", gameObject);
            }
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
            isChangingSize = true;
            gameObject.transform.localScale = Vector3.Lerp(currentSize, targetSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        isChangingSize = false;
        gameObject.transform.localScale = targetSize;
    }

    void GrowObject()
    {
        Vector3 currentSize = GetComponent<Transform>().localScale;

        if (forceOnSizeChange)
        {           //This adds some force to objects when they grow for visual player feedback
            if (GetComponent<Rigidbody>() != null)
            {
                Vector3 upForce = new Vector3(Random.Range(-0.90f, 0.90f), 1f, Random.Range(-0.90f, 0.90f));
                if (currentSize != maxSize) gameObject.GetComponent<Rigidbody>().AddRelativeForce(upForce * 7.77f * forceMultiplier);
                else gameObject.GetComponent<Rigidbody>().AddRelativeForce(upForce * 10f * forceMultiplier);
            }
        }

        if (currentSize != maxSize)
        {
            //gameObject.transform.localScale = maxSize;
            GetComponent<Stats>().Weight = GetComponent<Stats>().Weight * 5f;
            StartCoroutine(LerpSize(currentSize, maxSize, changeDuration));
            shrunk = false;
            if (gameObject.tag == "Player")
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("playerGrow", gameObject);
            }
            else
            {
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("objectGrow", gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        ///only does increase
       // GrowObject();       //Taken out due to switch bug (not sure of use)

        if (other.tag == "SizeOverride")
        {
            if (tag == "Player")
            {
                GrowObject();
            }
        }
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

    public bool Pickupable()
    {
        return canBePickedUp;
    }
}
