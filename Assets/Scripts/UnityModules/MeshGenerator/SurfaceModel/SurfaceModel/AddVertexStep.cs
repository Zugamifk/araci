using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class AddVertexStep : IStep
    {
        Vector3 _point;
        Vertex _vertex;

        public AddVertexStep(Vector3 point) => _point = point;

        public string Label => $"Add vertex at {_point}";

        public void Do(SurfaceModelBuilder builder)
        {
            _vertex = builder.AddPoint(_point);
        }

        public void Undo(SurfaceModelBuilder builder)
        {
            builder.RemovePoint(_vertex);
        }
    }
}
