using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModel : IActionModel
{
    public Guid Id { get; }= Guid.NewGuid();
    public string Key { get; set; }
    public Guid SourceId { get; set; }
    public CooldownModel Cooldown { get; set; } = new();
    public AnimationStateModel AnimationState { get; set; } = new();
}
