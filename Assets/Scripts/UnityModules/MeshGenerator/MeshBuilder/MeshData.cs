using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public class MeshData
    {
        public List<Vector3> Vertices = new();
        public List<Vector3> Normals = new();
        public List<Vector2> Uvs = new();
        public List<Color> Colors = new();
        public List<int> Triangles = new();
        public List<BoneWeight1> BoneWeights = new();

        public void Clear()
        {
            Vertices.Clear();
            Normals.Clear();
            Uvs.Clear();
            Colors.Clear();
            Triangles.Clear();
            BoneWeights.Clear();
        }
    }
}
