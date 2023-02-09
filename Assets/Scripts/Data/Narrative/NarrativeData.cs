using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    [CreateAssetMenu(menuName ="Narratives/New Narrative")]
    public class NarrativeData : ScriptableObject
    {
        public string Key;
        public NarrativeState StartState;

        public Dictionary<Guid, NarrativeState> IdtoState = new();

        private void OnEnable()
        {
            var state = StartState;
            while(state!=null)
            {
                IdtoState.Add(state.Id, state);
                state = state.Next;
            }
        }
    }
}