using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinerURP : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float scale;
    [SerializeField] private Color outlineColour;
    private Renderer outlinerenderer;

    void Start()
    {
        outlinerenderer = CreateOutline(outlineMaterial, scale, outlineColour);
        outlinerenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color colour)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMaterial;
        rend.material.SetColor("_OutlineColour", colour);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<OutlinerURP>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = true;

        return rend;
    }
}
