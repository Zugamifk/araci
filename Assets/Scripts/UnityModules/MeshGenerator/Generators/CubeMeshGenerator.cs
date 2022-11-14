using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    [MeshGenerator("Cube")]
    public class CubeMeshGenerator : MeshGenerator
    {
        static readonly Vector3[] _points = new[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,1),
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(0,1,1),
            new Vector3(1,1,1),
            new Vector3(1,1,0)
        };

        protected override void BuildMesh(MeshBuilder builder)
        {
            var p0 = _points[0];
            var p1 = _points[1];
            var p2 = _points[2];
            var p3 = _points[3];
            var p4 = _points[4];
            var p5 = _points[5];
            var p6 = _points[6];
            var p7 = _points[7];

            builder.AddQuad(p0, p3, p2, p1);
            builder.AddQuad(p0, p4, p7, p3);
            builder.AddQuad(p0, p1, p5, p4);
            builder.AddQuad(p1, p2, p6, p5);
            builder.AddQuad(p3, p7, p6, p2);
            builder.AddQuad(p4, p5, p6, p7);
        }
    }
}
