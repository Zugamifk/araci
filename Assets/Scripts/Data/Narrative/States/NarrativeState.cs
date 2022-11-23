using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public abstract class NarrativeState : ScriptableObject
    {
        public string Name;
        public NarrativeState Next;
    }
}