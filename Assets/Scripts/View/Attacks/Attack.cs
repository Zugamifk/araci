using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;

public class Attack : ModelViewBase<IAttackModel>
{
    List<Guid> _attackTargets = new();

    public override IAttackModel GetModel() => Game.Model.Attacks.GetItem(Id);

    public override void InitializeFromModel(IAttackModel model)
    {
        var source = ViewLookup.Get(model.SourceId);
        var player = source.GetComponent<Player>();
        player.DoAttack(this, model);
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
            Game.Do(new ApplyAttackEffect(Id, new List<Guid>(_attackTargets)));
            _attackTargets.Clear();
        }
    }
}
