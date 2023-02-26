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
        NarrativeActionData[] actions;

        public string Key => key;
        public int ActionCount => actions.Length;
        public NarrativeActionData GetActionData(int index) => actions[index];
    }
}