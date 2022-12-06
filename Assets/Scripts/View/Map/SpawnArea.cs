using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    string _key;
    [SerializeField]
    Bounds _bounds;

    private void Start()
    {
        var m = transform.localToWorldMatrix;
        var corners = new Vector2[4];
        var min = _bounds.min;
        var max = _bounds.max;
        corners[0] = Map.Instance.WorldToGridSpace(m.MultiplyPoint3x4(new Vector3(min.x, min.y,0)));
        corners[1] = Map.Instance.WorldToGridSpace(m.MultiplyPoint3x4(new Vector3(min.x, max.y,0)));
        corners[2] = Map.Instance.WorldToGridSpace(m.MultiplyPoint3x4(new Vector3(max.x, max.y,0)));
        corners[3] = Map.Instance.WorldToGridSpace(m.MultiplyPoint3x4(new Vector3(max.x, min.y,0)));
        Game.Do(new RegisterSpawn(_key, corners));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(150, 150, 255);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube( _bounds.center, _bounds.size );
    }
}
