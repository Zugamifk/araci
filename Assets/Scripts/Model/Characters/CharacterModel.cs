using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ICharacterModel
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public string DisplayName { get; }
    public float MoveSpeed { get; set; } = 10;
    public HealthModel Health { get; set; } = new();
    public ActionModel CurrentAction { get; set; } = new();
    public AttackModel Attack { get; set; } = new();
    
    IHealthModel ICharacterModel.Health => Health;
    IActionModel ICharacterModel.CurrentAction => CurrentAction;
    IAttackModel ICharacterModel.Attack => Attack;
}
