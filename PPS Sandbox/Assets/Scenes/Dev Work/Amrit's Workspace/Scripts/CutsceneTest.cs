using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTest : MonoBehaviour
{
    public DialogueCutsceneClass dialogueCutscene;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        dialogueCutscene.ObjectCutsceneView();

        yield return new WaitForSeconds(3);

        dialogueCutscene.PlayerCutsceneView();

        yield return new WaitForSeconds(3);

        dialogueCutscene.ReturnCamToOrigin();
    }
}
