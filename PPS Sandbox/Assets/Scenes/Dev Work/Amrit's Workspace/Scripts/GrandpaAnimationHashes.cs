using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaAnimationHashes : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public int isSittingBool;
    public int isIdleBool;
    public int isPickingUpBool;
    public int isPuttingDownBool;

    private void Awake()
    {
        //set the ID references from the animator
        animator = gameObject.GetComponent<Animator>();

        //bools
        isSittingBool = Animator.StringToHash("isSitting");
        isIdleBool = Animator.StringToHash("isIdle");
        isPickingUpBool = Animator.StringToHash("isPickingUp");
        isPuttingDownBool = Animator.StringToHash("isPuttingDown");
    }
}
