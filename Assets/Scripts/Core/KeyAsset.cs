using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Key Asset", menuName = "Key Asset")]
public class KeyAsset : ScriptableObject
{
    [SerializeField]
    string key;

    public string Key => key;

    public override string ToString() => key;

    public static implicit operator string(KeyAsset keyAsset) => keyAsset.Key;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(Key))
        {
            key = name;
        }
    }
}
