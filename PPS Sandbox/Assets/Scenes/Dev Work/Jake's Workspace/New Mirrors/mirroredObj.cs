using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirroredObj : MonoBehaviour
{
    public Transform objToCopy;
    public Transform mirror;
    public bool isPlayer;
    public GameObject player;
    public Animator _animator;

    public Animator pAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pAnim = player.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 localObj = mirror.InverseTransformPoint(objToCopy.position);
        transform.position = mirror.TransformPoint(new Vector3(localObj.x, -localObj.y, localObj.z));
        transform.rotation = Quaternion.Euler(objToCopy.rotation.x, -objToCopy.eulerAngles.y, objToCopy.rotation.z);
        transform.localScale = objToCopy.localScale;


        //in your level change all player animators to PlayerAnimator from Amrit folder
        //this includes the mirrored player animator in each mirror and the actual player animator

        // 1 = idle
        // 2 = forward
        // 3 = backward
        // 4 = strafe right
        // 5 = strafe left

        /*if (isPlayer)
        {
            if (player.GetComponent<FirstPersonController>().isWalking)
            {
                if (player.GetComponent<FirstPersonController>().currentInput.x > 0.2)
                {
                    _animator.SetFloat("Decider", 4);  // 4 = strafe right
                }
                if (player.GetComponent<FirstPersonController>().currentInput.x < -0.2)
                {
                    _animator.SetFloat("Decider", 5); // 5 = strafe left
                }
                if (player.GetComponent<FirstPersonController>().currentInput.y > 0.2)
                {
                    _animator.SetFloat("Decider", 2); // 2 = forward
                }
                if (player.GetComponent<FirstPersonController>().currentInput.y < -0.2)
                {
                    _animator.SetFloat("Decider", 3); // 3 = backward
                }
            }
            else
            {
                _animator.SetFloat("Decider", 1); // 1 = idle
            }
        }*/

        if(isPlayer)
        {
            _animator.SetFloat("Decider", pAnim.GetFloat("Decider"));
            _animator.SetBool("IsJumping", pAnim.GetBool("IsJumping"));
            _animator.SetBool("IsGrounded", pAnim.GetBool("IsGrounded"));
            _animator.SetBool("IsFalling", pAnim.GetBool("IsFalling"));
        }
    }
}
