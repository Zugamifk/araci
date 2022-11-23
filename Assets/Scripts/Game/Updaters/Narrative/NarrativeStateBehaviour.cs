using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NarrativeStateBehaviour
{
    public bool IsDone { get; protected set; }
    public void EnterState(NarrativeState data)
    {
        IsDone = false;
        OnEnterState(data);
    }
    protected abstract void OnEnterState(NarrativeState data);
    public abstract void Update();
}

public abstract class NarrativeStateBehaviour<TStateData> : NarrativeStateBehaviour
    where TStateData : NarrativeState
{
    protected TStateData _data;
    protected sealed override void OnEnterState(NarrativeState data)
    {
        _data = (TStateData)data;
    }
}
