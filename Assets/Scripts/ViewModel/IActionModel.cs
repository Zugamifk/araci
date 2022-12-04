using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionModel : IKeyHolder, IIdentifiable
{
    Vector2 TargetPosition { get;  }
    ICooldownModel Cooldown { get; }
    IAnimationStateModel AnimationState { get; }
}
