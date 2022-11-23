using Narrative;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNarrative : ICommand
{
    string _key;

    public StartNarrative(string key)
    {
        _key = key;
    }

    public void Execute(GameModel model)
    {
        var id = Guid.NewGuid();
        var data = DataService.GetData<NarrativeDataCollection>().GetData(_key);
        model.Narratives.AddItem(new NarrativeModel()
        {
            Id = id,
            NarrativeKey = _key,
            CurrentStateKey = data.StartState.Name
        });
        Game.AddUpdater(id, new NarrativeUpdater(id));
    }
}
