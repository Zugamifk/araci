using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationData : ScriptableObject
{
    [System.Serializable]
    public class ClipData
    {
        public string Name;
        public Sprite[] Sprites;
        public float Duration;
        public AnimationClip Clip;
    }

    public string Name;
    public Animator Animator;
    public GameObject Prefab;
    public ClipData[] Clips;
}
