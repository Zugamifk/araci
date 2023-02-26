using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName ="Narratives/New Narrative")]
    public class NarrativeData : ScriptableObject, IKeyHolder
    {
        [SerializeField]
        string displayName;
        [SerializeField, TextArea]
        string description;
        [SerializeField]
        string key;
        [SerializeField]
        NarrativeStateData[] states;

        public string Key => key;
        public int StateCount => states.Length;
        public NarrativeStateData GetStateData(int index) => states[index];
    }
}