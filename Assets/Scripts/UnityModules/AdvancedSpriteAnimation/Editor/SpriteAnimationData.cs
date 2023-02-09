using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpriteAnimationData : ScriptableObject
{
    [System.Serializable]
    public class ClipData
    {
        public string Name = "New Clip Data";
        public Texture Source;
        public int StartIndex;
        public int FrameCount = 1;
        public float Duration = 1;
        public bool Loop;
        public bool IsDefaultState;
        public AnimationClip Clip;
    }

    public string Name;
    public AnimatorController Controller;
    public GameObject Prefab;
    public List<ClipData> Clips = new();

    Dictionary<string, ClipData> _nameToClip = new();

    private void OnEnable()
    {
        foreach(var cd in Clips)
        {
            _nameToClip[cd.Name] = cd;  
        }
    }

    public ClipData GetClipData(string name) => _nameToClip[name];
}
