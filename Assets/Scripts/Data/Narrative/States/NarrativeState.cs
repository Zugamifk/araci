using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrative
{
    public abstract class NarrativeState : ScriptableObject
    {
        [SerializeField]
        SerializableGuid _guid;
        [SerializeField, TextArea]
        string _description;
        [SerializeField]
        NarrativeState _next;

        public Guid Id => _guid.Guid;
        public NarrativeState Next => _next;
    }
}