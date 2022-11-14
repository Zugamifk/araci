using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IInputModel
{
    Guid CurrentMouseOverObject { get; }
    Vector3 ClickPosition { get; }
    Guid WorldMapInputHandlerId { get; }
}
