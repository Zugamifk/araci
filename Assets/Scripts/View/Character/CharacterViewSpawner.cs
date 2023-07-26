using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewSpawner : RegisteredPrefabViewSpawner<ICharacterModel, Character>
{
    protected override IIdentifiableLookup<ICharacterModel> collection => Game.Model.Characters;
}
