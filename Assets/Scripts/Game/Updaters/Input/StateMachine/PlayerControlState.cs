using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Input;

namespace Input
{
    public class PlayerControlState : InputState
    {
        public override void Update(IInputStateMachine inputStateMachine)
        {
            if(Game.Model.PlayerCharacter == null)
            {
                inputStateMachine.SetState(new InactiveState());
                return;
            } else if (Game.Model.Dialog!=null)
            {
                inputStateMachine.PushState(new DialogState());
                return;
            }

            UpdateHotkeys();
            UpdateMovement();
            UpdateAttacks();
            UpdateActions(inputStateMachine);
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
            movement = mapService.WorldToGridSpace(movement.normalized);
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

        void UpdateActions(IInputStateMachine inputStateMachine)
        {
            if (GetKeyDown(KeyCode.E))
            {
                var interactable = Game.Model.Input.CurrentInteractable;
                if (interactable.Value != null)
                {
                    var interactionService = Services.Get<IInteractionService>();
                    interactionService.ProcessInteraction(interactable.Value);
                }
            }

            var cooldownService = Services.Get<ICooldownService>();

            if (cooldownService.IsReady(Game.Model.Player.Dash.Cooldown) && GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
                inputStateMachine.PushState(new PlayerDoingActionState(Game.Model.Player.Dash.Duration));
            }
        }

        void Dash()
        {
            var player = Game.Model.PlayerCharacter;
            var movement = Game.Model.Movements[player.Id];
            Game.Do(new DoDash(Game.Model.Player.Id, movement.Direction.Value, Game.Model.Player.Dash.Speed));
        }

        void UpdateHotkeys()
        {
            if (!anyKeyDown)
            {
                return;
            }

            var uiSvc = Services.Get<IUIService>();
            foreach(var key in uiSvc.GetPanelHotkeys())
            {
                if (GetKeyDown(key))
                {
                    uiSvc.PressedPanelHotkey(key);
                }
            }
        }
    }
}