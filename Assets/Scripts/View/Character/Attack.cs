using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    List<Guid> _attackTargets = new();

    public event Action<IEnumerable<Guid>> OnUpdatedAttackTargets;

    void OnEnable()
    {
        _attackTargets.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy!=null)
        {
            var ident = enemy.GetComponent<Identifiable>();
            _attackTargets.Add(ident.Id);
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
