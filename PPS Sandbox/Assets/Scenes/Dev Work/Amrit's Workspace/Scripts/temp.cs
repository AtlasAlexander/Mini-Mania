using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public DialogueCutsceneClass dialogueCutscene;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        dialogueCutscene.ObjectCutsceneView();

        yield return new WaitForSeconds(2);

        dialogueCutscene.PlayerCutsceneView();

        yield return new WaitForSeconds(2);

        dialogueCutscene.ReturnCamToOrigin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
