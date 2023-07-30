using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Input
{
    public class InputUpdater : IUpdater, IInputStateMachine
    {
        InputStateContext stateContext;
        InputState currentState;
        Stack<InputState> stateStack = new();

        public InputUpdater()
        {
            stateContext = new();
            // put start state here
            currentState = new PlayerControlState() { Context = stateContext };
        }

        public void Update(GameModel model)
        {
            currentState.Update(this);
            UpdateInteractables(model);
        }

        public void SetState(InputState state)
        {
            currentState = state;
        }

        public void PushState(InputState state)
        {
            stateStack.Push(currentState);
            SetState(state);
        }

        public void PopState()
        {
            if(stateStack.Count == 0)
            {
                throw new InvalidOperationException($"State stack is empty, can not pop state.");
            }

            currentState = stateStack.Pop();
        }

        void UpdateInteractables(GameModel model)
        {
            Guid closest = Guid.Empty;
            float closestDistance = float.MaxValue;
            var player = model.Positions.GetItem(model.Player.Id);
            if (player == null)
            {
                return;
            }

            Vector2 playerPosition =  player.Position.Value;
            foreach (var id in model.Input.InteractableTargets)
            {
                var pos = Game.Model.Positions[id];
                var distance = (playerPosition - pos.Position.Value).sqrMagnitude;
                if (closest == Guid.Empty || distance < closestDistance)
                {
                    closest = id;
                }
            }

            if(closest!= null)
            {
                model.Input.CurrentInteractable.Value = closest;
            }
        }
    }
}