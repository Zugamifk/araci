using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoDash : ICommand
{
    public void Execute(GameModel model)
    {
        var player = model.Player;
        var character = model.Characters.GetItem(model.Player.Id);
        var dir = character.Movement.Direction;
        character.Movement.DesiredMove = dir * player.Dash.Speed;
        player.Dash.IsDashing = true;
    }
}
