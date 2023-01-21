using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModel : IAttackModel
{
    public float Range { get; set; }
    public int Damage { get; set; }
    public CooldownModel Cooldown { get; set; } = new();

    public float WindUpTime;
    public float AttackTime;
    public float WindDownTime;

    ICooldownModel IAttackModel.Cooldown => Cooldown;
}
