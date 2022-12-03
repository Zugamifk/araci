using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModel : IAttackModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Key { get; set; }
    public Guid SourceId { get; set; }
    public Vector2 TargetPosition { get; set; }
    public int Damage { get; set; }
}
