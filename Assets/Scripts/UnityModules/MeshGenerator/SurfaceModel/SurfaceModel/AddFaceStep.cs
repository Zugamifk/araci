using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public class AddFaceStep : IStep
    {
        int[] _vertices;
        Face _face;

        public AddFaceStep(params int[] indices)
        {
            _vertices = indices;
            Label = $"Create face for vertices {string.Join(", ", indices)}";
        }

        public string Label { get; }

        public void Do(SurfaceModelBuilder builder)
        {
            _face = builder.CreateFace(_vertices);
        }

        public void Undo(SurfaceModelBuilder builder)
        {
            builder.RemoveFace(_face);
        }
    }
}
