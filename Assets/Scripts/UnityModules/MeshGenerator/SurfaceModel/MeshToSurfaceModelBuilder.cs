using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class MeshToSurfaceModelBuilder
    {
        public IEnumerable<SurfaceModel> ConvertMeshEnumerable(Mesh mesh)
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            List<Vector3> points = new();
            var tris = mesh.triangles;
            var verts = mesh.vertices;
            int[] meshPointToSurfaceModelPoint = new int[verts.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                meshPointToSurfaceModelPoint[i] = i;
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j] == verts[i])
                    {
                        meshPointToSurfaceModelPoint[i] = j;
                    }
                }
                if (meshPointToSurfaceModelPoint[i] == i)
                {
                    meshPointToSurfaceModelPoint[i] = points.Count;
                    points.Add(verts[i]);
                }
            }
            Debug.Log(points.Count);

            foreach (var v in points)
            {
                builder.AddPoint(v);
            }

            for (int i = 0; i < tris.Length; i += 3)
            {
                var p0 = meshPointToSurfaceModelPoint[tris[i]];
                var p1 = meshPointToSurfaceModelPoint[tris[i + 1]];
                var p2 = meshPointToSurfaceModelPoint[tris[i + 2]];
                Debug.Log($"Add Tri {p0} {p1} {p2}");
                builder.CreateFace(p0, p1, p2);
                yield return model;
            }

            Debug.Log(model.Edges.Count);

            yield return model;
        }
        public SurfaceModel ConvertMesh(Mesh mesh)
        {
            var model = new SurfaceModel();
            var builder = new SurfaceModelBuilder(model);

            List<Vector3> points = new();
            var tris = mesh.triangles;
            var verts = mesh.vertices;
            int[] meshPointToSurfaceModelPoint = new int[verts.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                meshPointToSurfaceModelPoint[i] = i;
                for (int j = 0; j < points.Count; j++)
                {
                    if (points[j] == verts[i])
                    {
                        meshPointToSurfaceModelPoint[i] = j;
                    }
                }
                if (meshPointToSurfaceModelPoint[i] == i)
                {
                    meshPointToSurfaceModelPoint[i] = points.Count;
                    points.Add(verts[i]);
                }
            }
            Debug.Log(points.Count);

            foreach (var v in points)
            {
                builder.AddPoint(v);
            }

            for (int i = 0; i < tris.Length; i += 3)
            {
                var p0 = meshPointToSurfaceModelPoint[tris[i]];
                var p1 = meshPointToSurfaceModelPoint[tris[i + 1]];
                var p2 = meshPointToSurfaceModelPoint[tris[i + 2]];
                Debug.Log($"Add Tri {p0} {p1} {p2}");
                builder.CreateFace(p0, p1, p2);
            }

            Debug.Log(model.Edges.Count);

            return model;
        }
    }
}
