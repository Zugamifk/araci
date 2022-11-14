using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugx
{
    public static void DrawCircle(Vector3 position, Vector3 normal, float radius, int lineSegments = 32)
    {
        Vector3 dir = Vector3.Cross(normal, Random.onUnitSphere).normalized;

        float step = 360 / lineSegments;
        var ang = Quaternion.AngleAxis(step, normal);
        for(int i=0;i<lineSegments;i++)
        {
            var next = ang * dir;
            var start = position + dir * radius;
            var end = position + next * radius;
            Debug.DrawLine(start, end);
            dir = next;
        }
    }
}
