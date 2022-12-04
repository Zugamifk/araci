using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModel : IActionModel
{
    public Guid Id { get; }= Guid.NewGuid();
    public string Key { get; set; }
    public Vector2 TargetPosition { get; set; }
    public CooldownModel Cooldown { get; set; } = new();
    public AnimationStateModel AnimationState { get; set; } = new();

    ICooldownModel IActionModel.Cooldown => Cooldown;
    IAnimationStateModel IActionModel.AnimationState => AnimationState;
}
