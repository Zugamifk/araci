using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Surfaces
{
    public interface IStep
    {
        string Label { get; }
        void Do(SurfaceModelBuilder builder);
        void Undo(SurfaceModelBuilder builder);
    }
}
