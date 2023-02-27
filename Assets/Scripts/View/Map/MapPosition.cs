using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapPosition : MonoBehaviour
{
    [SerializeField]
    string key;

    private void Start()
    {
        var mapPosition = Map.Instance.WorldToGridSpace(transform.position);
        Game.Do(new AddLocation(key, mapPosition));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .25f);
        Handles.Label(transform.position+new Vector3(-.5f, .5f, 0), key);
    }
}
