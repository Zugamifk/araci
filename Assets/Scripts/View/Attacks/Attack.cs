using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : ModelViewBase<IAttackModel>
{
    public override IAttackModel GetModel() => Game.Model.Attacks.GetItem(Id);

    public override void InitializeFromModel(IAttackModel model)
    {
        var source = Game.Model.Movement.GetItem(model.SourceId);
        Map.Instance.PositionObject(source, transform);
    }
}
