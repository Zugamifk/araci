using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class Vertex
    {
        public string Label;
        public Vector3 Position;
        public HalfEdge HalfEdge;

        public IEnumerable<Edge> Edges()
        {
            if (HalfEdge == null) yield break;

            var start = HalfEdge;
            var he = start;
            int max = 100;
            do
            {
                yield return he.Edge;
                he = he.Twin.Next;
            } while (max-- > 0 && he != start);
        }

        public IEnumerable<HalfEdge> HalfEdges()
        {
            if (HalfEdge == null) yield break;

            var start = HalfEdge;
            var he = start;
            int max = 100;
            do
            {
                yield return he;
                yield return he.Twin;
                he = he.Twin.Next;
            } while (max-- > 0 && he != start);
        }

        public IEnumerable<HalfEdge> ExitingHalfEdges()
        {
            if (HalfEdge == null) yield break;

            var start = HalfEdge;
            var he = start;
            int max = 100;
            do
            {
                yield return he;
                he = he.Twin.Next;
            } while (max-- > 0 && he != start);
        }

        public IEnumerable<HalfEdge> EnteringHalfEdges()
        {
            if (HalfEdge == null) yield break;

            var start = HalfEdge.Twin;
            var he = start;
            int max = 100;
            do
            {
                yield return he;
                he = he.Twin.Next;
            } while (max-- > 0 && he != start);
        }

        public override string ToString()
        {
            return $"{Label} {Position}";
        }
    }
}
