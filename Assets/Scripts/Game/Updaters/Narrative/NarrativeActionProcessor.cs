using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NarrativeActionProcessor
{
    public bool IsFinished { get; protected set; }

    public virtual void OnStart(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data) { }
    public virtual void OnUpdate(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data) { }
    public virtual void OnFinish(GameModel gameModel, NarrativeModel narrativeModel, NarrativeActionData data) { }

}
