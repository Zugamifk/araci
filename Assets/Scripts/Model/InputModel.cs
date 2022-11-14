using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModel : IInputModel
{
    public Guid CurrentMouseOverObject { get; set; }
    public Vector3 ClickPosition { get; set; }
    public Guid WorldMapInputHandlerId { get; } = Guid.NewGuid(); 
}
