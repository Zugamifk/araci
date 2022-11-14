using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    [SerializeField]
    string _key;

    public string Key => _key;
}
