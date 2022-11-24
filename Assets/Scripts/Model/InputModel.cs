using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel : IInputModel
{
    public Guid CurrentMouseOverObject { get; set; }
    public Vector3 ClickPosition { get; set; }
    public Dictionary<Guid,InteractableModel> InteractableTargets { get; set; } = new();
    public InteractableModel CurrentInteractable { get; set; }

    Guid IInputModel.CurrentInteractableId => CurrentInteractable?.Id ?? Guid.Empty;
}
