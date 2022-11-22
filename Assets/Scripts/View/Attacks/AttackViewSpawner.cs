using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackViewSpawner : RegisteredPrefabViewSpawner<IAttackModel, Attack>
{
    protected override IEnumerable<IAttackModel> AllModels() => Game.Model.Attacks.AllItems;

    protected override IAttackModel GetModel(Guid id) => Game.Model.Attacks.GetItem(id);
}
