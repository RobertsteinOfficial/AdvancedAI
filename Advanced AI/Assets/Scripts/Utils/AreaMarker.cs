using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMarker : MonoBehaviour
{
    public float height, width;
    [Range(0,1)]
    public float hue;

    private void OnDrawGizmos()
    {
        Color c = Color.HSVToRGB(hue, 1, 1);
        c.a = 0.6f;
        Gizmos.color = c;
        Gizmos.DrawCube(transform.position, new Vector3(width, 0, height));
    }
}
