using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class BevelOperation
    {
        class SplitInfo
        {
            public Vertex Base;
            public List<Vertex> Split = new();
        }

        Dictionary<Vertex, SplitInfo> _splitVertexLookup = new();

        SurfaceModelBuilder _builder;

        public BevelOperation(SurfaceModelBuilder builder) => _builder = builder;

        public void BevelEdges(float amount, params Edge[] edges)
        {
            _splitVertexLookup.Clear();

            foreach (var e in edges)
            {
                SplitEdge(e);
            }

            foreach(var v in _splitVertexLookup.Keys)
            {
                ExplodeVertex(v);
            }
        }

        void SplitEdge(Edge edge)
        {
            _builder.RemoveEdge(edge);
        }

        void ExplodeVertex(Vertex vertex)
        {

        }
    }
}
