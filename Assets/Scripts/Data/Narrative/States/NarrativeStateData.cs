using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public abstract class NarrativeStateData : ScriptableObject
    {
        [SerializeField]
        string displayName;
        [SerializeField, TextArea]
        string description;

        public string DisplayName => displayName;
        public string Description => description;
    }
}