using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HandleX
{
    public static void DrawArrow(Vector3 from, Vector3 to, Vector3 normal, float size)
    {
        var dir = (to - from).normalized;
        var perp = Vector3.Cross(dir, normal);
        Handles.DrawLine(from, to);
        Handles.DrawLine(to - dir * size + perp * size, to);
        Handles.DrawLine(to - dir * size - perp * size, to);
    }
}
