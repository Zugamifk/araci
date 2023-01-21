using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

namespace Input
{
    public class PlayerControlState : InputState
    {
        public override InputState Update()
        {
            UpdateMovement();
            UpdateAttacks();
            return UpdateActions();
        }

        void UpdateMovement()
        {
            Vector2 movement = Vector2.zero;
            if (GetKey(KeyCode.W))
            {
                movement += Vector2.up;
            }
            if (GetKey(KeyCode.S))
            {
                movement += Vector2.down;
            }
            if (GetKey(KeyCode.A))
            {
                movement += Vector2.left;
            }
            if (GetKey(KeyCode.D))
            {
                movement += Vector2.right;
            }

            var mapService = Services.Get<ITileMapService>();
            movement = mapService.WorldToGridSpace(movement);
            Game.Do(new MoveCharacter(Game.Model.Player.Id, movement));
        }

        void UpdateAttacks()
        {
            if (GetMouseButtonDown(0))
            {
                var cooldownService = Services.Get<ICooldownService>();
                if (cooldownService.IsReady(Game.Model.PlayerCharacter.Attack.Cooldown))
                {
                    Game.Do(new DoAttack(Game.Model.Player.Id, Camera.main.ScreenToWorldPoint(mousePosition)));
                }
            }
        }

        InputState UpdateActions()
        {
            if (GetKeyDown(KeyCode.E))
            {
                var interactable = Game.Model.Input.CurrentInteractableId;
                if (interactable != Guid.Empty)
                {
                    Game.Do(new UseInteractable(interactable));
                }
            }

            if (GetKeyDown(KeyCode.LeftShift))
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
}