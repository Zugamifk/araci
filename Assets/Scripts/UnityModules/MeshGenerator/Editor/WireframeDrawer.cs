using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshGenerator.Wireframes;
using UnityEditor;

namespace MeshGenerator.Editor
{
    public static class WireframeDrawer
    {
        public static void Draw(Wireframe frame)
        {
            foreach(var e in frame.Edges)
            {
                Handles.DrawLine(e.A, e.B);
            }

            foreach(var r in frame.Rings)
            {
                Handles.DrawWireDisc(r.Center, r.Normal, r.Radius);
            }
        }
    }
}
