using UnityEngine;

namespace MeshGenerator
{
    public class MeshPreviewer : MonoBehaviour
    {
        [SerializeField]
        Mesh _mesh;
        [SerializeField]
        string _meshType;
        [SerializeField]
        Transform _generatorTransform;

        GeneratedSkinnedMeshRenderer _renderer;
        GeneratedSkinnedMeshRenderer Renderer => _renderer ??= GetComponentInChildren<GeneratedSkinnedMeshRenderer>();

        public IGeometryGenerator CurrentGenerator { get; private set; }

        public void SetMesh(MeshGeneratorResult result)
        {
            Renderer.ApplyMesh(result);
        }

        public void Clear()
        {
            Renderer.Clear();
        }
    }
}
