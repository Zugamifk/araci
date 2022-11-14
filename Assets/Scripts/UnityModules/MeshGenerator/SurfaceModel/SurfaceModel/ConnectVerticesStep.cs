using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class ConnectVerticesStep : IStep
    {
        class CreateHalfEdgeStep : IStep
        {
            int _index;
            HalfEdge _halfEdge;
            public HalfEdge HalfEdge => _halfEdge;
            public int Index => _index;
            public string Label => $"Create HalfEdge On Vertex {_index}";
            public CreateHalfEdgeStep(int index)
            {
                _index = index;
            }

            public void Do(SurfaceModelBuilder builder)
            {
                _halfEdge = builder.CreateHalfEdge(_index);
            }

            public void Undo(SurfaceModelBuilder builder)
            {
                builder.RemoveHalfEdge(_halfEdge);
            }
        }
        class CreateEdgeStep : IStep
        {
            CreateHalfEdgeStep _h0, _h1;
            Edge _edge;
            public CreateEdgeStep(CreateHalfEdgeStep h0, CreateHalfEdgeStep h1)
            {
                _h0 = h0;
                _h1 = h1;
            }

            public string Label => $"Create Edge from {_h0.Index} to {_h1.Index}";

            public void Do(SurfaceModelBuilder builder)
            {
                _edge = builder.CreateEdge(_h0.HalfEdge, _h1.HalfEdge);
            }

            public void Undo(SurfaceModelBuilder builder)
            {
                builder.RemoveEdge(_edge);
            }
        }

        class UpdateHalfEdgeConnectionsStep : IStep
        {
            CreateHalfEdgeStep _h0, _h1;
            public UpdateHalfEdgeConnectionsStep(CreateHalfEdgeStep h0, CreateHalfEdgeStep h1)
            {
                _h0 = h0;
                _h1 = h1;
            }

            public string Label => $"Update HalfEdge connections from {_h0.Index} to {_h1.Index}";

            public void Do(SurfaceModelBuilder builder)
            {
                var e0 = _h0.HalfEdge;
                var e1 = _h1.HalfEdge;
                var from = e0.Vertex.EnteringHalfEdges().FirstOrDefault(he => he.Face == e1.Face);
                if (from != null)
                {
                    var to = from.Next;
                    from.Next = e0;
                    e1.Next = to;
                }
                else
                {
                    Debug.LogWarning($"No half eadge leading to {e0}!");
                }
            }

            public void Undo(SurfaceModelBuilder builder)
            {
                var e0 = _h0.HalfEdge;
                var e1 = _h1.HalfEdge;
                var from = e0.Vertex.EnteringHalfEdges().FirstOrDefault(he => he.Face == e1.Face);
                e1.Next = e0;
                from.Next = from.Twin;
            }
        }

        class UpdateVertexStep : IStep
        {
            int _index;
            CreateHalfEdgeStep _step;

            public UpdateVertexStep(int index, CreateHalfEdgeStep step)
            {
                _index = index;
                _step = step;

            }

            public string Label => $"Set HalfEdge for vertex {_index} to {_step.HalfEdge}";

            public void Do(SurfaceModelBuilder builder)
            {
                builder.Model.Vertices[_index].HalfEdge = _step.HalfEdge;
            }

            public void Undo(SurfaceModelBuilder builder)
            {
                builder.Model.Vertices[_index].HalfEdge = _step.HalfEdge.Twin?.Next;
            }
        }

        int _i0, _i1;

        public string Label => $"Connect vertices {_i0} and {_i1}";

        public static IEnumerable<IStep> InSubsteps(int i0, int i1)
        {
            var he0 = new CreateHalfEdgeStep(i0);
            yield return he0;
            var he1 = new CreateHalfEdgeStep(i1);
            yield return he1;
            var edge = new CreateEdgeStep(he0, he1);
            yield return edge;
            yield return new UpdateHalfEdgeConnectionsStep(he0, he1);
            yield return new UpdateHalfEdgeConnectionsStep(he1, he0);
            yield return new UpdateVertexStep(i0, he0);
            yield return new UpdateVertexStep(i1, he1);
        }

        public ConnectVerticesStep(int i0, int i1) {
            _i0 = i0;
            _i1 = i1;
        }

        public void Do(SurfaceModelBuilder builder)
        {
            builder.ConnectVertices(_i0, _i1);
        }

        public void Undo(SurfaceModelBuilder builder)
        {
            builder.DisconnectVertices(_i0, _i1);
        }
    }
}
