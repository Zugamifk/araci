using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class HalfEdge
    {
        public string Label;
        public HalfEdge Twin;
        public HalfEdge Next;
        public Vertex Vertex;
        public Edge Edge;
        public Face Face;

        public HalfEdge Previous => Vertex.HalfEdges().FirstOrDefault(he => he.Next == this);
        public Vertex StartVertex => Vertex;
        public Vertex EndVertex => Twin?.StartVertex;

        static HashSet<HalfEdge> _visited = new();
        public IEnumerable<HalfEdge> Loop()
        {
            _visited.Clear();
            var start = this;
            var next = start;
            do
            {
                yield return next;
                _visited.Add(next);
                next = next.Next;

                if (next == start)
                {
                    yield break;
                }
            } while (!_visited.Contains(next));

            throw new System.InvalidOperationException($"Error enumerating loop! Found an internal loop at {next}");
        }

        public override string ToString()
        {
            return $"{Label} -> {Next?.Label} [Twin: {Twin?.Label}]";
        }
    }
}
