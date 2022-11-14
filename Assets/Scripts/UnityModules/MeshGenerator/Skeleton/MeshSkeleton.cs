using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshGenerator.Skeleton
{
    public class MeshSkeleton : MonoBehaviour
    {
        [SerializeField]
        MeshBone[] _bones;

        public MeshBone Root { get; private set; }

        public IReadOnlyList<MeshBone> Bones => _bones;

        Dictionary<string, MeshBone> _nameToBone;
        
        public MeshBone GetBone(string key)
        {
            if(_nameToBone == null)
            {
                CacheBones();
            }

            return _nameToBone[key];
        }
        
        void CacheBones()
        {
            _nameToBone = new();
            foreach (var b in _bones)
            {
                _nameToBone[b.name] = b;
                if (b.IsRoot) Root = b;
            }
        }
    }
}
