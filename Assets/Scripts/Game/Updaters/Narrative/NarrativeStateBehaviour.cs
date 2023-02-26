using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NarrativeStateBehaviour
{
    public bool IsFinished { get; protected set; }

    public virtual void OnEnterState(GameModel gameModel, NarrativeModel narrativeModel, NarrativeStateData data) { }
    public virtual void OnUpdateState(GameModel gameModel, NarrativeModel narrativeModel, NarrativeStateData data) { }
    public virtual void OnExitState(GameModel gameModel, NarrativeModel narrativeModel, NarrativeStateData data) { }

}
