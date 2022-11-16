using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewSpawner : RegisteredPrefabViewSpawner<ICharacterModel, Character>
{
    protected override IEnumerable<ICharacterModel> AllModels() => Game.Model.Characters.AllItems;

    protected override ICharacterModel GetModel(Guid id) => Game.Model.Characters.GetItem(id);

    protected override void SpawnedView(ICharacterModel model, Character view)
    {
        ViewLookup.Register(model.Id, view.gameObject);
    }

    protected override void DestroyedView(Character view)
    {
        ViewLookup.Remove(view.Id);
    }
}
