using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class InputUpdater : IUpdater
    {
        InputStateContext stateContext;
        InputState state;

        public InputUpdater()
        {
            stateContext = new();
            // put start state here
            state = new PlayerControlState() { Context = stateContext };
        }

        public void Update(GameModel model)
        {
            state = state.Update();
            UpdateInteractables(model);
        }

        void UpdateInteractables(GameModel model)
        {
            InteractableModel closest = null;
            float closestDistance = float.MaxValue;
            var player = model.Characters.GetItem(model.Player.Id);
            if (player == null)
            {
                return;
            }

            Vector2 playerPosition = player.Movement.Position;
            foreach (var interactable in model.Input.InteractableTargets.Values)
            {
                var distance = (playerPosition - interactable.Position).sqrMagnitude;
                if (closest == null || distance < closestDistance)
                {
                    closest = interactable;
                }
            }
            model.Input.CurrentInteractable = closest;
        }
    }
}