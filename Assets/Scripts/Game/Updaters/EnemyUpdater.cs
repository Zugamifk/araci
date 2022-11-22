using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdater : IUpdater
{
    Guid _id;
    public EnemyUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        var enemy = model.Characters.GetItem(_id);
        if(enemy == null)
        {
            Game.RemoveUpdater(_id);
            return;
        }

        var player = model.Characters.GetItem(model.Player.Id);
        var dir = player.Movement.Position - enemy.Movement.Position;
        Game.Do(new MoveCharacter(_id, dir.normalized, Space.Grid));
    }
}
