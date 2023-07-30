using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RemoveInteractable : ICommand
{
    Guid id;

    public RemoveInteractable(Guid id)
    {
        this.id = id;
    }

    public void Execute(GameModel model)
    {
        model.Input.InteractableTargets.Remove(id);
    }
}
