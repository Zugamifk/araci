using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationData : ScriptableObject
{
    [System.Serializable]
    public class ClipData
    {
        public string Name;
        public Texture Source;
        public int StartIndex;
        public int FrameCount;
        public float Duration;
        public bool Loop;
        public AnimationClip Clip;
    }

    public string Name;
    public Animator Animator;
    public GameObject Prefab;
    public List<ClipData> Clips = new();
}
