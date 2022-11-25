using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Transform _attackRoot;
    [SerializeField]
    DashEffect _dash;

    bool _wasDashing;
    private void Update()
    {
        var isDashing = Game.Model.Player.Dash.IsDashing;
        if (_wasDashing!=isDashing)
        {
            if(isDashing)
            {
                _dash.DoDash();
            }
            _wasDashing = isDashing;
        }
    }

    public void DoAttack(Attack attack, IAttackModel model)
    {
        var attackTransform = attack.transform;
        attackTransform.SetParent(_attackRoot);
        attackTransform.localPosition = Vector3.zero;

        var dir = model.TargetPosition - (Vector2)_attackRoot.position;
        attackTransform.rotation = Math.PointAt(dir.normalized);
    }
}
