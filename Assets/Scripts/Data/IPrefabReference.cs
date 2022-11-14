using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefabReference
{
    string Name { get; }
    GameObject Prefab { get; }
}
