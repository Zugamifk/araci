using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace MeshGenerator.Wireframes
{
    public class Wireframe
    {
        public List<Edge> Edges = new();
        public List<Ring> Rings = new();

        Stack<Matrix4x4> _stack = new();
        Matrix4x4 _matrix = Matrix4x4.identity;

        public void PushMatrix(Matrix4x4 matrix)
        {
            _stack.Push(_matrix);
            _matrix = _matrix * matrix;
        }

        public void PopMatrix()
        {
            if(_stack.Count > 0 )
            {
                _matrix = _stack.Pop();
            }
        }

        public void Clear()
        {
            Edges.Clear();
            Rings.Clear();
        }

        public void Connect(Vector3 a, Vector3 b)
        {
            a = _matrix.MultiplyPoint3x4(a);
            b = _matrix.MultiplyPoint3x4(b);
            Edges.Add(new Edge() { A = a, B = b });
        }

        public void Connect(params Vector3[] points)
        {
            for (int i = 1; i < points.Length; i++)
            {
                Connect(points[i - 1], points[i]);
            }
        }

        public void ConnectLoop(params Vector3[] points)
        {
            Connect(points);
            Connect(points[0], points[points.Length - 1]);
        }

        public void AddRing(Vector3 baseCentre, float radius, Vector3 normal)
        {
            baseCentre = _matrix.MultiplyPoint3x4(baseCentre);
            normal = _matrix.MultiplyVector(normal);
            Rings.Add(new Ring()
            {
                Center = baseCentre,
                Radius = radius,
                Normal = normal
            });
        }

        public void Prism(Vector3 baseCentre, float height, int sides, float radius, Vector3 direction)
        {
            var rot = Quaternion.identity;
            var step = Quaternion.AngleAxis(360 / (float)sides, direction);
            var baseDir = Vector3.Cross(new Vector3(.5f, 0, .5f), direction).normalized * radius;
            for (int i = 0; i < sides; i++)
            {
                var cr = rot;
                var p0 =baseCentre + cr * baseDir;
                var p1 =baseCentre + step * cr * baseDir;
                Connect(p0, p1);
                var p2 = p0 + direction * height;
                Connect(p0, p2);
                var p3 = p1 + direction * height;
                Connect(p2, p3);
                rot *= step;
            }
        }

        public void Prism(IList<Vector3> points, Vector3 normal, float height)
        {
            for(int i = 0; i <points.Count;i++)
            {
                var p0 = points[i];
                var p1 = points[(i + 1) % points.Count];
                Connect(p0, p1);
                var p2 = p0 + normal * height;
                var p3 = p1 + normal * height;
                Connect(p2, p3);
                Connect(p0, p2);
            }
        }

        public void SquareColumn(Vector3 baseCentre, float height, float size)
        {
            var p0 = baseCentre+ new Vector3(-size, 0, -size);
            var p1 = baseCentre+ new Vector3(-size, 0, size);
            var p2 = baseCentre+ new Vector3(size, 0, size);
            var p3 = baseCentre+ new Vector3(size, 0, -size);
            var p4 = baseCentre+ new Vector3(-size, height, -size);
            var p5 = baseCentre+ new Vector3(-size, height, size);
            var p6 = baseCentre+ new Vector3(size, height, size);
            var p7 = baseCentre+ new Vector3(size, height, -size);

            Connect(p0, p1);
            Connect(p1, p2);
            Connect(p2, p3);
            Connect(p3, p0);

            Connect(p0, p4);
            Connect(p1, p5);
            Connect(p2, p6);
            Connect(p3, p7);

            Connect(p4, p5);
            Connect(p5, p6);
            Connect(p6, p7);
            Connect(p7, p4);
        }

        public void Cuboid(float width, float height, float depth)
        {
            var w = width / 2;
            var h = height / 2;
            var d = depth / 2;
            var p0 = new Vector3(-w, -h, -d);
            var p1 = new Vector3(-w, -h, d);
            var p2 = new Vector3(w, -h, d);
            var p3 = new Vector3(w, -h, -d);
            var p4 = new Vector3(-w, h, -d);
            var p5 = new Vector3(-w, h, d);
            var p6 = new Vector3(w, h, d);
            var p7 = new Vector3(w, h, -d);

            Connect(p0, p1);
            Connect(p1, p2);
            Connect(p2, p3);
            Connect(p3, p0);

            Connect(p0, p4);
            Connect(p1, p5);
            Connect(p2, p6);
            Connect(p3, p7);

            Connect(p4, p5);
            Connect(p5, p6);
            Connect(p6, p7);
            Connect(p7, p4);
        }

        public void Cylinder(Vector3 baseCentre, float radius, float length, Vector3 normal)
        {
            AddRing(baseCentre, radius, normal);
            AddRing(baseCentre + normal * length, radius, normal);

            var edgeTangent = SceneView.lastActiveSceneView.camera.transform.forward;
            var edge = Vector3.Cross(edgeTangent, normal).normalized;
            var p0 = baseCentre + edge * radius;
            var p1 = baseCentre + -edge * radius;
            var p2 = baseCentre + edge * radius + normal * length;
            var p3 = baseCentre + -edge * radius + normal * length;
            Connect(p0, p2);
            Connect(p1, p3);
        }
    }
}
