using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanPower;
    private GameObject fanHeightMarker;
    private float fanPosY;
    private float fanHeightMarkerPosY;
    private float fanHeight;
    private float fanHeightRatio;

    private void Start()
    {
        fanPosY = transform.position.y;
        fanHeightMarker= transform.GetChild(0).gameObject;
        fanHeightMarkerPosY = fanHeightMarker.transform.position.y;
        fanHeight = fanHeightMarkerPosY - fanPosY;
        //fanHeightRatio = 1 / fanHeight;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if(other.gameObject.transform.position.y < fanHeightMarkerPosY)
            {
                fanHeightRatio = 1 - ((1 / fanHeight) * other.gameObject.transform.position.y);

                other.GetComponent<CharacterController>().Move(Vector3.up * fanPower * fanHeightRatio * Time.deltaTime);

                Debug.Log(fanHeightRatio);
            }
        }
    }
}
