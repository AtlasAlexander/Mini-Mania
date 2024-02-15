using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirroredObj : MonoBehaviour
{
    public Transform objToCopy;
    public Transform mirror;
    public bool isPlayer;
    public GameObject Player;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 localObj = mirror.InverseTransformPoint(objToCopy.position);
        transform.position = mirror.TransformPoint(new Vector3(localObj.x, -localObj.y, localObj.z));
        transform.rotation = Quaternion.Euler(objToCopy.rotation.x, -objToCopy.eulerAngles.y, objToCopy.rotation.z);
        transform.localScale = objToCopy.localScale;
        if(isPlayer)
        {
            if(Player.GetComponent<FirstPersonController>().isWalking)
            {
                _animator.SetFloat("Forward", 1);
            }
            else
            {
                _animator.SetFloat("Forward", 0);
            }
        }
    }
}
