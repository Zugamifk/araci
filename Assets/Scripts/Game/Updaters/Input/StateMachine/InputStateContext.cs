using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStateContext
{
    public Dictionary<Guid, IInputHandler> IdToHandler = new();
}
