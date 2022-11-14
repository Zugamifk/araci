using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator
{
    public class MeshBone : MonoBehaviour
    {
        [SerializeField]
        string _key;
        [SerializeField]
        bool _isRoot;

        public string Key => _key;
        public bool IsRoot => _isRoot;

        public Matrix4x4 GetBindPose()
        {
            if(_isRoot)
            {
                return Matrix4x4.identity;
            } else
            {
                return transform.worldToLocalMatrix * transform.parent.localToWorldMatrix;
            }
        }
    }
}
