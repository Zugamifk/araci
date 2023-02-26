using Narrative;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NarrativeUpdater : IUpdater
{
    #region Populate Behaviour Lookup
    static Dictionary<Type, NarrativeStateBehaviour> _dataTypeToBehaviour = new();
    static NarrativeUpdater()
    {
        RegisterBehaviour<SpawnEnemiesStateBehaviour, SpawnEnemiesStateData>();
    }

    static void RegisterBehaviour<TNarrativeStateBehaviour, TNarrativeStateData>()
        where TNarrativeStateBehaviour : NarrativeStateBehaviour, new()
        where TNarrativeStateData : NarrativeStateData
    {
        _dataTypeToBehaviour[typeof(TNarrativeStateData)] = new TNarrativeStateBehaviour();
    }
    #endregion

    Guid id;
    public NarrativeUpdater(Guid id)
    {
        this.id = id;
    }

    public void Update(GameModel model)
    {
        var narrative = model.Narratives.GetItem(id);
        if (narrative == null)
        {
            throw new InvalidOperationException($"No narrative with id {id}");
        }

        var narrativeData = DataService.GetData<NarrativeDataCollection>().GetData(narrative.NarrativeKey);

        if (narrative.CurrentStateIndex < 0)
        {
            EnterNextState(model, narrative, narrativeData);
        }

        var stateData = narrativeData.GetStateData(narrative.CurrentStateIndex);
        var behaviour = _dataTypeToBehaviour[stateData.GetType()];

        behaviour.OnUpdateState(model, narrative, stateData);

        if (behaviour.IsFinished)
        {
            behaviour.OnExitState(model, narrative, stateData);

            EnterNextState(model, narrative, narrativeData);
        }
    }

    void EnterNextState(GameModel gameModel, NarrativeModel narrativeModel, NarrativeData narrativeData)
    {
        narrativeModel.CurrentStateIndex++;
        if (narrativeModel.CurrentStateIndex < narrativeData.StateCount)
        {
            var stateData = narrativeData.GetStateData(narrativeModel.CurrentStateIndex);
            var behaviour = _dataTypeToBehaviour[stateData.GetType()];

            behaviour.OnEnterState(gameModel, narrativeModel, stateData);
        }
        else
        {
            gameModel.Narratives.RemoveItem(id);
            Game.RemoveUpdater(id);
        }
    }
}
