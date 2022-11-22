using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ICharacterModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public float MoveSpeed { get; set; } = 10;
    public MovementModel Movement { get; set; } = new();
    public HealthModel Health { get; set; } = new();

    IMovementModel ICharacterModel.Movement => Movement;
}
