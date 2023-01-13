using Codice.Client.BaseCommands;
using PlasticPipe.PlasticProtocol.Messages;
using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlState : InputState
{
    public override IState UpdateState()
    {
        UpdateMovement();
        UpdateAttacks();
        return UpdateActions();
    }

    void UpdateMovement()
    {
        Vector2 movement = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right;
        }

        var mapService = Services.Get<ITileMapService>();
        movement = mapService.WorldToGridSpace(movement);
        Game.Do(new MoveCharacter(Game.Model.Player.Id, movement));
    }
    
    void UpdateAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Game.Model.PlayerCharacter.CurrentAction.Cooldown.IsReady())
            {
                Game.Do(new DoAttack(Game.Model.Player.Id, Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            }
        }
    }

    IState UpdateActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var interactable = Game.Model.Input.CurrentInteractableId;
            if (interactable != Guid.Empty)
            {
                Game.Do(new UseInteractable(interactable));
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
            return new PlayerDoingActionState(Game.Model.Player.Dash.Duration);
        }

        return this;
    }

    void Dash()
    {
        var player = Game.Model.PlayerCharacter;
        Game.Do(new DoDash(Game.Model.Player.Id, player.Movement.Direction, Game.Model.Player.Dash.Speed));
    }
}
