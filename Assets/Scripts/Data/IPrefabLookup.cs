using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrefabLookup
{
    GameObject GetPrefab(string key);
}
