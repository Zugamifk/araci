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
    }
}