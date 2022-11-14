using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class Edge
    {
        public string Label;
        public HalfEdge HalfEdge;

        public bool IsConnectedTo(Edge other)
        {
            var current = HalfEdge.Next;
            while(current.Edge!=this)
            {
                if(current.Edge == other)
                {
                    return true;
                }
                current = current.Twin.Next;
            }

            current = HalfEdge.Twin.Next;
            while (current.Edge != this)
            {
                if (current.Edge == other)
                {
                    return true;
                }
                current = current.Twin.Next;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Label}: {HalfEdge.Vertex} -> {HalfEdge.Twin.Vertex}";
        }
    }
}
