using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    string _key;
    [SerializeField]
    Bounds _bounds;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(150, 150, 255);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube( _bounds.center, _bounds.size );
    }
}
