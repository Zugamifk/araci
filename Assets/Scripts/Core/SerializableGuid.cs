using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableGuid : ISerializationCallbackReceiver
{
    [SerializeField, ReadOnly]
    string _serializedValue;

    Guid _guid;

    public Guid Guid => _guid;

    public void OnAfterDeserialize()
    {
        if(!Guid.TryParse(_serializedValue, out _guid))
        {
            _guid = Guid.NewGuid();
            _serializedValue = _guid.ToString();
        }
    }

    public void OnBeforeSerialize()
    {
        if(_guid == Guid.Empty)
        {
            _guid = Guid.NewGuid();
            _serializedValue = _guid.ToString();
        }
    }

}
