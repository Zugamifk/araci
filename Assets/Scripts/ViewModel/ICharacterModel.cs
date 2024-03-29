using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterModel : IIdentifiable, IKeyHolder
{
    string DisplayName { get; }
    IAttackModel Attack { get; }
    IActionModel CurrentAction { get; }
    IMovementModel Movement { get; }
    IHealthModel Health { get; }
}
