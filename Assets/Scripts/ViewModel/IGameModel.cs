using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    IIdentifiableLookup<IMovementModel> Movement { get; }
    IIdentifiableLookup<ICharacterModel> Characters { get; }
    IPlayerModel Player { get; }
    IInputModel Input { get; }
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
}
