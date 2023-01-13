using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputState
{
    public InputStateContext Context { get; set; }

    public virtual void Initialize()
    {
    }

    public abstract InputState Update();
}
