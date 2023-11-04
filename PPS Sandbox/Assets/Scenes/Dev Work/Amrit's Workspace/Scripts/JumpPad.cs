using UnityEngine;

public class JumpPad : MonoBehaviour
{

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("IT_Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger entered");
            //rb.AddForce(Vector2.up * playerControllerScript.jumpForce * 2.5f);
            // playerControllerScript.jumpForce = 20;
            // playerControllerScript.moveDir.y = playerControllerScript.jumpForce;
            playerControllerScript.jumpHeight = 3;
            playerControllerScript.playerVelocity.y += Mathf.Sqrt(playerControllerScript.jumpHeight * playerControllerScript.force * playerControllerScript.gravityValue);
            playerControllerScript.playerVelocity.y += playerControllerScript.gravityValue * Time.deltaTime;
            playerControllerScript.controller.Move(playerControllerScript.playerVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("trigger exit");
            //rb.AddForce(Vector2.up * playerControllerScript.jumpForce * 2.5f);
            // playerControllerScript.jumpForce = 8;

            playerControllerScript.jumpHeight = 1;
        }
    }
}