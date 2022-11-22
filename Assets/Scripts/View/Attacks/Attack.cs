using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ModelViewBase<IAttackModel>
{
    public override IAttackModel GetModel() => Game.Model.Attacks.GetItem(Id);

    public override void InitializeFromModel(IAttackModel model)
    {
        var source = ViewLookup.Get(model.SourceId);
        var player = source.GetComponent<Player>();
        player.DoAttack(this, model);
    }
}
