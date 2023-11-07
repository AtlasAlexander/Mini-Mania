using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public float Intensity = 1f;
    public float Mass = 1f;
    public float Stiffness = 1f;
    public float Damping = 0.7f;
    private Mesh originalMesh, meshClone;
    private MeshRenderer renderer;
    private JellyVertex[] jv;
    private Vector3[] vertixArray;

    private void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        renderer = GetComponent<MeshRenderer>();
        jv = new JellyVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; i++)
        {
            jv[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
        }
    }

    private void FixedUpdate()
    {
        vertixArray = originalMesh.vertices;
        for (int i = 0; i < jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertixArray[jv[i].ID]);
            float intensity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size.y) * Intensity;
            jv[i].Shake(target, Mass, Stiffness, Damping);
            target = transform.InverseTransformPoint(jv[i].Position);
            vertixArray[jv[i].ID] = Vector3.Lerp(vertixArray[jv[i].ID], target, intensity);
        }
        meshClone.vertices = vertixArray;
    }

    public class JellyVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 Velo, Force;
        public JellyVertex(int _id, Vector3 pos)
        {
            ID = _id;
            Position = pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            Velo = (Velo + Force / m) * d;
            Position += Velo;
            if ((Velo + Force + Force / m).magnitude < 0.001f)
            {
                Position = target;
            }
        }
    }
}
