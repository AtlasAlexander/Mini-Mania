using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSave : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("Checkpoint");
    }
}
