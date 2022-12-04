using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    IIdentifiableLookup<ICharacterModel> Characters { get; }
    IIdentifiableLookup<IShrineModel> Shrines { get; }
    IPlayerModel Player { get; }
    ICharacterModel PlayerCharacter { get; }
    IInputModel Input { get; }
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
}
