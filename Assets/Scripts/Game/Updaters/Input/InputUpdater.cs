using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputUpdater : IUpdater
{
    StateMachine _inputStateMachine;
    InputStateContext _inputStateContext;

    public InputUpdater()
    {
        _inputStateContext = new();
        // put start state here
        _inputStateMachine = new StateMachine(new PlayerControlState() { Context = _inputStateContext });
    }

    public void Update(GameModel model)
    {
        _inputStateMachine.Update();
        UpdateInteractables(model);
    }

    void UpdateInteractables(GameModel model)
    {
        InteractableModel closest = null;
        float closestDistance = float.MaxValue;
        Vector2 playerPosition = model.Characters.GetItem(model.Player.Id).Movement.Position;
        foreach(var interactable in model.Input.InteractableTargets.Values)
        {
            var distance = (playerPosition - interactable.Position).sqrMagnitude;
            if(closest == null || distance < closestDistance)
            {
                closest = interactable;
            }
        }
        model.Input.CurrentInteractable = closest;
    }
}
