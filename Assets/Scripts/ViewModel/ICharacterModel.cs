using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel : IIdentifiable, IKeyHolder
{
    IActionModel CurrentAction { get; }
    IMovementModel Movement { get; }
    IHealthModel Health { get; }
}
