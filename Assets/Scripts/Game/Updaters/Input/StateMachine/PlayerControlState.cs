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
        UpdateActions();

        return this;
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

        Game.Do(new MoveCharacter(Game.Model.Player.Id, movement, Space.World));
    }
    
    void UpdateAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Game.Do(new DoAttack(Game.Model.Player.Id, "Sword", Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
    }

    void UpdateActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var interactable = Game.Model.Input.CurrentInteractableId;
            if (interactable != Guid.Empty)
            {
                Game.Do(new UseInteractable(interactable));
            }
        }
    }
}
