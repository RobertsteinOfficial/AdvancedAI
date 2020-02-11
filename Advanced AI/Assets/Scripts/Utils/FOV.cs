using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FOV : MonoBehaviour
{
    [Header("Sight")]
    public bool showMesh = true;
    [Range(3,90)]
    public int resolution = 10;
    public float eyeHeight = 1.6f;
    public float sightRange = 4f;
    public float sightAngle = 120;

    public Vector3 EyesPosition
    {
        get
        {
            return transform.position + Vector3.up * eyeHeight;
        }
    }

    //LOCAL
    Mesh mesh;
    MeshRenderer rend;
    List<Vector3> vertices;
    List<int> triangles;

    void Start()
    {
        mesh = new Mesh();
        rend = GetComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void GetVertices()
    {
        //preparo lista vuota
        vertices = new List<Vector3>();
        //calcolo step di angoli
        float step = sightAngle / resolution;
        //aggiungo vertice 0
        vertices.Add( transform.InverseTransformPoint(EyesPosition));
        //raycast per ogni step di angolo
        Ray ray;
        RaycastHit hit;
        for (float a = -sightAngle / 2; a < sightAngle / 2; a += step)
        {
            ray = new Ray(EyesPosition, Angle2Vector(a, transform.rotation));
            //se colpisce, aggiungo punto di interruzione
            if (Physics.Raycast(ray, out hit, sightRange))
                vertices.Add(transform.InverseTransformPoint(hit.point));
            else //altrimenti aggiungo punto finale
                vertices.Add(transform.InverseTransformPoint(ray.GetPoint(sightRange)));
        }
        //ultimo ray
        ray = new Ray(EyesPosition, Angle2Vector(sightAngle/2, transform.rotation));
        //se colpisce, aggiungo punto di interruzione
        if (Physics.Raycast(ray, out hit, sightRange))
            vertices.Add(transform.InverseTransformPoint(hit.point));
        else //altrimenti aggiungo punto finale
            vertices.Add(transform.InverseTransformPoint(ray.GetPoint(sightRange)));

        //Debug.DrawLine(EyesPosition,Angle2Vector(-sightAngle / 2, transform.rotation) * sightRange);
        //Debug.DrawLine(EyesPosition, Angle2Vector(0, transform.rotation) * sightRange);
        //Debug.DrawLine(EyesPosition, Angle2Vector(sightAngle / 2, transform.rotation) * sightRange);
    }

    void GenerateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();

        triangles = new List<int>();
        for (int i = 0; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void Update()
    {
        rend.enabled = showMesh;
        if (!showMesh)
            return;

        GetVertices();
        GenerateMesh();
    }

    //public static Vector3 Angle2Vector(float angle)
    //{
    //    float x = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
    //    float z = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));

    //    return new Vector3(x, 0, z);
    //}
    public static Vector3 Angle2Vector(float angle, Quaternion frontRot)
    {
        float x = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
        float z = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));

        return frontRot * new Vector3(x, 0, z);
    }
    //public static Vector3 Angle2Vector(float angle, bool isLocal, Transform master = null)
    //{
    //    float x = Mathf.Cos(Mathf.Deg2Rad * (angle + 90));
    //    float z = Mathf.Sin(Mathf.Deg2Rad * (angle + 90));
    //    if (isLocal)
    //    {
    //        return master.TransformVector(new Vector3(x, 0, z));
    //    }
    //    else
    //        return new Vector3(x, 0, z);
    //}
}
