using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialog")]
public class DialogData : ScriptableObject, IKeyHolder
{
    [SerializeField]
    KeyAsset key;
    [SerializeField]
    KeyAsset speakerKey;
    [SerializeField, TextArea]
    string[] lines;

    public string Key => key;
    public string SpeakerKey => speakerKey;
    public string[] Lines => lines;
}
