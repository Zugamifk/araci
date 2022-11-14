using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MeshGenerator
{
    public class MeshGeneratorResult
    {
        public Dictionary<string, Matrix4x4> SpecialBones = new();
        public Mesh Mesh;
    }
}
