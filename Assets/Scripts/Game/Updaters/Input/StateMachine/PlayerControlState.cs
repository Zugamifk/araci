using StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlState : InputState
{
    public override IState UpdateState()
    {
        UpdateMovement();
        UpdateAttacks();

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

        }
    }
}
