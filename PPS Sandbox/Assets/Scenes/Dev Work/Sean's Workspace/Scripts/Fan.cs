using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float fanPower;
    [SerializeField] GameObject player;
    private float playerGravityValue;

    private void Start()
    {
        playerGravityValue = player.GetComponent<PlayerController>().GetGravityValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            other.GetComponent<PlayerController>().SetGravityValue(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            other.GetComponent<CharacterController>().Move(Vector2.up * fanPower * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            other.GetComponent<PlayerController>().SetGravityValue(playerGravityValue);
        }
    }
}
