using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderViewData : ScriptableObject, IKeyHolder
{
    [SerializeField] string key;
    [SerializeField] Color baseColor;

    public string Key => key;
    public Color BaseColor => baseColor;
}
