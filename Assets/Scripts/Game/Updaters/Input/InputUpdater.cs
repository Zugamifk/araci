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
            InteractableModel closest = null;
            float closestDistance = float.MaxValue;
            var player = model.Positions.GetItem(model.Player.Id);
            if (player == null)
            {
                return;
            }

            Vector2 playerPosition =  player.Position.Value;
            foreach (var interactable in model.Input.InteractableTargets.Values)
            {
                var distance = (playerPosition - interactable.Position).sqrMagnitude;
                if (closest == null || distance < closestDistance)
                {
                    closest = interactable;
                }
            }

            if(closest!= null)
            {
                model.Input.CurrentInteractable.Value = closest.Id;
            }
        }
    }
}