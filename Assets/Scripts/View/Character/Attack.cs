using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    TargetType targetType;

    List<Guid> _attackTargets = new();

    public event Action<IEnumerable<Guid>> OnUpdatedAttackTargets;

    void OnEnable()
    {
        _attackTargets.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.gameObject.GetComponent<ActionTarget>();
        if(target != null && target.IsType(targetType))
        {
            var ident = target.GetComponent<IObservable<Guid>>();
            _attackTargets.Add(ident.Value);
        }
    }

    private void Update()
    {
        if (_attackTargets.Count > 0)
        {
            OnUpdatedAttackTargets?.Invoke(_attackTargets);
            _attackTargets.Clear();
        }
    }
}
