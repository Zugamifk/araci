using Narrative;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeUpdater : IUpdater
{
    static Dictionary<Type, NarrativeStateBehaviour> _dataTypeToBehaviour = new();
    static NarrativeUpdater()
    {
        _dataTypeToBehaviour.Add(typeof(SpawnEnemiesState), new SpawnEnemiesStateBehaviour());
    }

    Guid _id;
    public NarrativeUpdater(Guid id)
    {
        _id = id;
    }

    public void Update(GameModel model)
    {
        var narrative = model.Narratives.GetItem(_id);
        if(narrative == null)
        {
            throw new InvalidOperationException($"No narrative with id {_id}");
        }
        if (narrative.CurrentStateId == Guid.Empty)
        {
            throw new InvalidOperationException($"Empty state! This should not be updating");
        }

        var data = DataService.GetData<NarrativeDataCollection>().GetData(narrative.NarrativeKey).IdtoState[narrative.CurrentStateId];
        var behaviour = _dataTypeToBehaviour[data.GetType()];
        Debug.Log("Enter " + data.name);
        behaviour.EnterState(data);
        behaviour.Update();

        var next = data.Next;
        if (next == null)
        {
            model.Narratives.RemoveItem(_id);
            Game.RemoveUpdater(_id);
        } else
        {
            narrative.CurrentStateId = next.Id;
        }
    }
}
