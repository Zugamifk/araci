using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ICharacterModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public float MoveSpeed { get; set; }
}
