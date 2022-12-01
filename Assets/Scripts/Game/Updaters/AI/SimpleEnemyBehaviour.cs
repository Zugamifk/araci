using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SimpleEnemyBehaviour : AgentBehaviour
{
    public override void Update(GameModel model, ICharacterModel agent)
    {
        var player = model.Characters.GetItem(model.Player.Id);
        var dir = player.Movement.Position - agent.Movement.Position;
        Game.Do(new MoveCharacter(Id, dir.normalized, Space.Grid));
    }
}
