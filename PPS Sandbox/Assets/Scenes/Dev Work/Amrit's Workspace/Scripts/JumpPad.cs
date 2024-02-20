using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private FirstPersonController firstPersonController;

    public float jumpForce = 16.0f;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            firstPersonController.moveDir.y = jumpForce;
        }


    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            firstPersonController.jumpForce = 8.0f;
        }
    }
}