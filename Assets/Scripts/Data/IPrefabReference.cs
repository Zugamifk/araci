using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefabReference : IKeyHolder
{
    GameObject Prefab { get; }
}
