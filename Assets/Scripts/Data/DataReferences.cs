using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This only exists to create a reference to data objects so they get their OnEnable() called
/// </summary>
[CreateAssetMenu(menuName="Data/Data References")]
public class DataReferences : ScriptableObject
{
    [SerializeField]
    ScriptableObject[] _references;

    private void OnEnable()
    {
        foreach(var r in _references)
        {
            DataService.Register(r);
        }
    }

    private void OnDisable()
    {
        DataService.Clear();
    }
}
