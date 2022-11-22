using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _attackRoot;

    public void DoAttack(Attack attack, IAttackModel model)
    {
        var attackTransform = attack.transform;
        attackTransform.SetParent(_attackRoot);
        attackTransform.localPosition = Vector3.zero;

        var dir = model.TargetPosition - (Vector2)_attackRoot.position;
        attackTransform.rotation = Math.PointAt(dir.normalized);
    }
}
