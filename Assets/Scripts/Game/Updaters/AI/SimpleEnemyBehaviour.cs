using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SimpleEnemyBehaviour : AgentBehaviour
{
    public SimpleEnemyBehaviour(Guid id) : base(id)
    {
    }

    public override void Update(GameModel model)
    {
        var player = model.Characters.GetItem(model.Player.Id);
        var agent = Game.Model.Characters.GetItem(_id);
        var dir = player.Movement.Position - agent.Movement.Position;
        Game.Do(new MoveCharacter(_id, dir.normalized, Space.Grid));
    }
}
