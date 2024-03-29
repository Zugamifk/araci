using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DoDash : ICommand
{
    Guid _id;
    Vector2 _direction;
    float _speed;

    public DoDash(Guid id, Vector2 direction, float speed)
    {
        _id = id;
        _direction = direction;
        _speed = speed;
    }

    public void Execute(GameModel model)
    {
        new MoveCharacter(_id, _direction, _speed).Execute(model);

        var action = new ActionModel()
        {
            Key = Actions.DASH
        };
        var character = model.Characters.GetItem(_id);
        character.CurrentAction = action;

        var cooldownService = Services.Get<ICooldownService>();
        cooldownService.StartCooldown(model.Player.Dash.Cooldown);
    }
}
