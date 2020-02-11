using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [Header("Gizmo")]
    public int gizmoIndex = 0;
    [Header("Generation")]
    public int seed = 0;
    public int dimension = 10;
    public float noiseStrength = 1;
    [Header("Materials")]
    public Material mat;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh mesh;

    private void OnValidate()
    {
        if (gizmoIndex < 0)
            gizmoIndex = 0;
        if (gizmoIndex >= mesh.vertices.Length)
            gizmoIndex = mesh.vertices.Length - 1;

        #region Controls
        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();
        if (!meshRenderer) meshRenderer = GetComponent<MeshRenderer>();
        if (!mesh) mesh = new Mesh();
        #endregion

        GenerateMesh();
    }


    void GenerateMesh()
    {
        System.Random pseudoRandom = new System.Random(seed);

        mesh.Clear();

        List<Vector3> vertList = new List<Vector3>();
        List<Vector2> uvList = new List<Vector2>();
        List<int> trisList = new List<int>();

        int vertCount = 0;
        for (int y = 0; y < dimension; y++)
        {
            for (int x = 0; x < dimension; x++)
            {
                //add vertex
                vertList.Add(new Vector3(x, (float)pseudoRandom.NextDouble() * noiseStrength, y));
                //add uv
                uvList.Add(new Vector2((float)x / dimension, (float)y / dimension));

                if (x < dimension - 1 && y < dimension - 1)
                {
                    //tris 1 (0 1 2)
                    trisList.Add(vertCount);
                    trisList.Add(vertCount + dimension);
                    trisList.Add(vertCount + dimension + 1);
                    //tris 2 (2 3 0)
                    trisList.Add(vertCount + dimension + 1);
                    trisList.Add(vertCount + 1);
                    trisList.Add(vertCount);
                }

                vertCount++;
            }
        }

        //calcoli
        //Vector3[] vertices = new Vector3[]
        //{
        //    new Vector3(0,0,0),
        //    new Vector3(0,0,1),
        //    new Vector3(1,0,1),
        //    new Vector3(1,0,0),
        //};
        //
        //int[] triangles = new int[]
        //{
        //    0, 1, 2,
        //    2, 3, 0
        //};

        //Vector2[] uvs = new Vector2[]
        //{
        //    new Vector2(0,0),
        //    new Vector2(0,1),
        //    new Vector2(1,1),
        //    new Vector2(1,0),
        //};

        mesh.vertices = vertList.ToArray();
        mesh.triangles = trisList.ToArray();
        mesh.uv = uvList.ToArray();

        mesh.RecalculateNormals();

        meshFilter.sharedMesh = mesh;
        mat.mainTexture.wrapMode = TextureWrapMode.Repeat;
        meshRenderer.material = mat;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(mesh.vertices[gizmoIndex], Vector3.one * 0.2f);
    }
}
