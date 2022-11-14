using MeshGenerator.Skeleton;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace MeshGenerator
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    [RequireComponent(typeof(MeshSkeleton))]
    public class GeneratedSkinnedMeshRenderer : MonoBehaviour
    {
        MeshSkeleton _skeleton_cached = null;
        MeshSkeleton _skeleton
        {
            get
            {
                if (_skeleton_cached == null)
                {
                    _skeleton_cached = GetComponent<MeshSkeleton>();
                }
                return _skeleton_cached;
            }
        }

        SkinnedMeshRenderer _meshRenderer_cached = null;
        SkinnedMeshRenderer _meshRenderer
        {
            get
            {
                if (_meshRenderer_cached == null)
                {
                    _meshRenderer_cached = GetComponent<SkinnedMeshRenderer>();
                }
                return _meshRenderer_cached;
            }
        }

        Transform[] GetBones()
        {
            var bones = _skeleton.Bones;
            var boneTfs = new Transform[bones.Count];
            for (int i = 0; i < bones.Count; i++)
            {
                boneTfs[i] = bones[i].transform;
            }
            return boneTfs;
        }

        public void ApplyMesh(MeshGeneratorResult result)
        {
            _meshRenderer.bones = GetBones();
            SetBindPoses(result.Mesh);
            _meshRenderer.sharedMesh = result.Mesh;

            foreach (var kv in result.SpecialBones)
            {
                var bone = _skeleton.GetBone(kv.Key);
                SetBoneMatrix(bone, kv.Value);
            }
        }

        void SetBindPoses(Mesh mesh)
        {
            var bindPoses = new Matrix4x4[_skeleton.Bones.Count];
            for (int i = 0; i < _skeleton.Bones.Count; i++)
            {
                bindPoses[i] = _skeleton.Bones[i].GetBindPose();
            }
            mesh.bindposes = bindPoses;
        }

        void SetBoneMatrix(MeshBone bone, Matrix4x4 matrix)
        {
            var mat = Matrix4x4.identity;
            if (!bone.IsRoot)
            {
                mat = _skeleton.Root.transform.localToWorldMatrix;
            }

            bone.transform.position = matrix.GetPosition();
            bone.transform.rotation = matrix.rotation;
        }

        public void Clear()
        {
            _meshRenderer.sharedMesh = null;
        }
    }
}
