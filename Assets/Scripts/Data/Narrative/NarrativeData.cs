using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName ="Narratives/New Narrative")]
    public class NarrativeData : ScriptableObject
    {
        public string Key;
        public NarrativeState StartState;

        public Dictionary<string, NarrativeState> NametoState = new();

        private void OnEnable()
        {
            var state = StartState;
            while(state!=null)
            {
                NametoState.Add(state.Name, state);
                state = state.Next;
            }
        }
    }
}