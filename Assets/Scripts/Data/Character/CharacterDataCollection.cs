using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataCollection : KeyHoldertoPrefabReferenceLookup<ICharacterModel, CharacterData>
{
    [SerializeField]
    CharacterData[] _characters;

    protected override IEnumerable<CharacterData> PrefabReferences => _characters;
}
