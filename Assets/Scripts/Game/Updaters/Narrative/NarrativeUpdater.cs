using Narrative;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NarrativeUpdater : IUpdater
{
    #region Populate Behaviour Lookup
    static Dictionary<Type, NarrativeActionProcessor> _dataTypeToActionProcessor = new();
    static NarrativeUpdater()
    {
        RegisterAction<SpawnEnemiesActionProcessor, SpawnEnemiesActionData>();
        RegisterAction<PositionCharacterActionProcessor, PositionCharacterActionData>();
    }

    static void RegisterAction<TNarrativeStateBehaviour, TNarrativeStateData>()
        where TNarrativeStateBehaviour : NarrativeActionProcessor, new()
        where TNarrativeStateData : NarrativeActionData
    {
        _dataTypeToActionProcessor[typeof(TNarrativeStateData)] = new TNarrativeStateBehaviour();
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

        if (narrative.CurrentActionIndex < 0)
        {
            BeginNextAction(model, narrative, narrativeData);
        }

        var stateData = narrativeData.GetActionData(narrative.CurrentActionIndex);
        var actionProcessor = _dataTypeToActionProcessor[stateData.GetType()];

        actionProcessor.OnUpdate(model, narrative, stateData);

        if (actionProcessor.IsFinished)
        {
            actionProcessor.OnFinish(model, narrative, stateData);

            BeginNextAction(model, narrative, narrativeData);
        }
    }

    void BeginNextAction(GameModel gameModel, NarrativeModel narrativeModel, NarrativeData narrativeData)
    {
        narrativeModel.CurrentActionIndex++;
        if (narrativeModel.CurrentActionIndex < narrativeData.ActionCount)
        {
            var actionData = narrativeData.GetActionData(narrativeModel.CurrentActionIndex);
            var action = _dataTypeToActionProcessor[actionData.GetType()];

            action.OnStart(gameModel, narrativeModel, actionData);
        }
        else
        {
            gameModel.Narratives.RemoveItem(id);
            Game.RemoveUpdater(id);
        }
    }
}
