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
        var enemy = model.Movement.GetItem(_id);
        if(enemy == null)
        {
            Game.RemoveUpdater(_id);
        }

        var player = model.Movement.GetItem(model.Player.Id);
        var dir = player.Position - enemy.Position;
        Debug.Log(dir);
        Game.Do(new MoveCharacter(_id, dir.normalized, Space.Grid));
    }
}
