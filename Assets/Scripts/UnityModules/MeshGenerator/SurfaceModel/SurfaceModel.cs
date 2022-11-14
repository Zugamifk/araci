using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class SurfaceModel
    {
        public List<Vertex> Vertices = new();
        public List<HalfEdge> HalfEdges = new();
        public List<Edge> Edges = new();
        public List<Face> Faces = new();

        public bool IsEmpty => Vertices.Count == 0 && HalfEdges.Count == 0 && Edges.Count == 0 && Faces.Count == 0;

        public void Clear()
        {
            Vertices.Clear();
            HalfEdges.Clear();
            Edges.Clear();
            Faces.Clear();
        }
    }
}
