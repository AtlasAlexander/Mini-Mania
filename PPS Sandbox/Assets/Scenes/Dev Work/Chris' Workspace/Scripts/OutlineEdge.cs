using UnityEngine;

public class OutlineEdge : MonoBehaviour
{
    private Camera mainCamera;
    private string edgeHighlightProperty = "_Scale";
    private MaterialPropertyBlock materialPropertyBlock;
    private Renderer highlightedRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                SetEdgeHighlightScale(hit.collider.GetComponent<Renderer>(), 1.05f);
            }
            else
            {
                SetEdgeHighlightScale(null, 0.0f);
            }
        }
        else
        {
            SetEdgeHighlightScale(null, 0.0f);
        }
    }

    void SetEdgeHighlightScale(Renderer renderer, float scale)
    {
        if (renderer != null)
        {
            if (highlightedRenderer != renderer)
            {
                if (highlightedRenderer != null)
                {
                    materialPropertyBlock.SetFloat(edgeHighlightProperty, 0.0f);
                    highlightedRenderer.SetPropertyBlock(materialPropertyBlock);
                }

                highlightedRenderer = renderer;
            }

            materialPropertyBlock.SetFloat(edgeHighlightProperty, scale);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
        else
        {
            if (highlightedRenderer != null)
            {
                materialPropertyBlock.SetFloat(edgeHighlightProperty, 0.0f);
                highlightedRenderer.SetPropertyBlock(materialPropertyBlock);
            }

            highlightedRenderer = null;
        }
    }
}
