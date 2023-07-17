using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableModel : IInteractableModel
{
    public Guid Id { get; set; }
    public Vector2 Position { get; set; }
}
