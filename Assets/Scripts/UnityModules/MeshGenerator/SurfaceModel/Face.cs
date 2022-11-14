using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class Face
    {
        /// <summary>
        /// This face represents the outside boundary of an open mesh.
        /// </summary>
        public static readonly Face Outside = new();

        public HalfEdge HalfEdge;

        public Vector3 Centre()
        {
            var start = HalfEdge;
            var he = start;
            Vector3 sum = Vector3.zero;
            float num = 0;
            do
            {
                sum += he.Vertex.Position;
                num++;
                he = he.Next;
            } while (he != start);
            if(num > 0) {
                return sum / num;
            } else
            {
                throw new System.InvalidOperationException($"No edges on this face!");
            }
        }
    }
}
