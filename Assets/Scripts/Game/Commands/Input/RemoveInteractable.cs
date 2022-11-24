using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RemoveInteractable : ICommand
{
    Guid _id;

    public RemoveInteractable(Guid id)
    {
        _id = id;
    }

    public void Execute(GameModel model)
    {
        model.Input.InteractableTargets.Remove(_id);
    }
}
