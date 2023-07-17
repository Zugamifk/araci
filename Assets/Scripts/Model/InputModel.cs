using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel : IInputModel
{
    public Dictionary<Guid,InteractableModel> InteractableTargets { get; set; } = new();
    public Observable<Guid> CurrentInteractable { get; set; } = new();
    public Observable<Vector2> ClickPosition { get; set; } = new();
    IObservable<Guid> IInputModel.CurrentInteractable => CurrentInteractable;

    IObservable<Vector2> IInputModel.ClickPosition => ClickPosition;
}
