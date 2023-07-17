using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject, IKeyHolder
{
    [SerializeField]
    KeyAsset key;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    string displayName;
    [SerializeField]
    string description;

    public string Key => key;
    public Sprite Icon => icon;
    public string DisplayName => displayName;
    public string Description => description;
}
