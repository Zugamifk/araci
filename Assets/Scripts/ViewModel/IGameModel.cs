using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    IIdentifiableLookup<ICharacterModel> Characters { get; }
    IIdentifiableLookup<IAttackModel> Attacks { get; }
    IPlayerModel Player { get; }
    IInputModel Input { get; }
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
}
