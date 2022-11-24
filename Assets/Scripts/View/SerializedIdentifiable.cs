using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class SerializedIdentifiable : Identifiable
{
    [CallMethodButton("GenerateId", "Regenerate Id")]
    [SerializeField, ReadOnly]
    string _serializedId;

    void OnValidate()
    {
        Guid id;
        if (string.IsNullOrEmpty(_serializedId) || !Guid.TryParse(_serializedId, out id))
        {
            id = GenerateId();
        }
        Id = id;
    }

    Guid GenerateId()
    {
        var id = Guid.NewGuid();
        _serializedId = id.ToString();
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
        return id;
    }
}
