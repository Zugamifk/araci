using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModel : IAttackModel
{
    public float Range { get; set; }
    public int Damage { get; set; }
    public float Cooldown { get; set; }
}
