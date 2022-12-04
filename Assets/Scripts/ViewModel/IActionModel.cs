using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionModel : IKeyHolder, IIdentifiable
{
    ICooldownModel Cooldown { get; }
    IAnimationStateModel AnimationState { get; }
}
