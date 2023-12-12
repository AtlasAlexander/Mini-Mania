using UnityEngine;

public class JumpPad : MonoBehaviour
{

    private FirstPersonController firstPersonControllerScript;

    void Start()
    {
        firstPersonControllerScript = GameObject.Find("IT_Player").GetComponent<FirstPersonController>();
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger entered");

            firstPersonControllerScript.moveDir.y = 24.0f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger exit");
        }
    }
}




