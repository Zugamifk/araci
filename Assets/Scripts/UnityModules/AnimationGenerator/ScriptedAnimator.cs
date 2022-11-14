using MeshGenerator;
using MeshGenerator.Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationGenerator
{
    public abstract class ScriptedAnimation : MonoBehaviour
    {
        [SerializeField] MeshSkeleton _skeleton;

        ScriptedAnimationPlayer _player;

        Coroutine _currentAnimation;

        public void SetAnimation(string boneKey, ScriptedAnimationData data)
        {
            var bone = _skeleton.GetBone(boneKey);
            _player = new ScriptedAnimationPlayer(data, x => UpdateAnimation(bone.transform, x));
        }

        public void PlayAnimation()
        {
            if(_currentAnimation!=null)
            {
                StopCoroutine(_currentAnimation);
            }
            _currentAnimation = StartCoroutine(_player.Play());
        }

        protected abstract void UpdateAnimation(Transform bone, float x);
    }
}
