using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AddInteractable : ICommand
{
    Guid id;

    public AddInteractable(Guid id)
    {
        this.id = id;
    }

    public void Execute(GameModel model)
    {
        model.Input.InteractableTargets.Add(id);
    }
}
