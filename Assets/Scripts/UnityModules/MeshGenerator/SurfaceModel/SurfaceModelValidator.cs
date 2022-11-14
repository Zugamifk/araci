using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class SurfaceModelValidator
    {
        class Exception : System.Exception { 
            public Exception(string message) : base(message) { }
        }
        class MissingHalfEdgeException : Exception
        {
            public MissingHalfEdgeException(string message) : base(message) { }
        }

        class MissingVertexException : Exception
        {
            public MissingVertexException(string message) : base(message) { }
        }

        class MissingEdgeException : Exception
        {
            public MissingEdgeException(string message) : base(message) { }
        }

        public void Validate(SurfaceModel model)
        {
            foreach(var he in model.HalfEdges)
            {
                ValidateHalfEdge(model, he);
            }
        }

        void ValidateHalfEdge(SurfaceModel model, HalfEdge he)
        {
            if(he.Twin==null)
            {
                throw new MissingHalfEdgeException($"HalfEdge {he} has no twin!");
            }

            if (he.Next == null)
            {
                throw new MissingHalfEdgeException($"HalfEdge {he} has no next!");
            }

            if(he.Vertex==null)
            {
                throw new MissingVertexException($"Halfedge {he} has no vertex!");
            }

            if(he.Edge==null)
            {
                throw new MissingVertexException($"Halfedge {he} has no vertex!");
            }

            if (!model.HalfEdges.Contains(he))
            {
                throw new MissingHalfEdgeException($"Model doesn't contain halfedge {he}!");
            }
        }

        void ValidateEdge(Edge e)
        {

        }
    }
}
