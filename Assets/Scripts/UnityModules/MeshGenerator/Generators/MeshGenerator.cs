using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public abstract class MeshGenerator : IGeometryGenerator
    {
        private MeshBuilder _builder = new();

        public MeshGeneratorResult Generate()
        {
            _builder.Clear();
            BuildMesh(_builder);
            return BuildResult();
        }

        protected virtual MeshGeneratorResult BuildResult()
        {
            var result = new MeshGeneratorResult();
            result.Mesh = _builder.BuildMesh();
            result.Mesh.RecalculateBounds();
            return result;
        }

        protected abstract void BuildMesh(MeshBuilder builder);
        public void Clear()
        {
            _builder.Clear();
        }
    }
}
