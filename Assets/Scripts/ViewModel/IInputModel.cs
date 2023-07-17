using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IInputModel
{
    IObservable<Guid> CurrentInteractable { get; }
    IObservable<Vector2> ClickPosition { get; }
}
