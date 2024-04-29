using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCutsceneScript : MonoBehaviour
{
    [SerializeField] GameObject eventSystem;


    // Start is called before the first frame update
    void Awake()
    {
        eventSystem.SetActive(false);
    }

    public void EnableEvents()
    {
        eventSystem.SetActive(true);
        Cursor.visible = true;
    }
}
