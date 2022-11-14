using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    IInputModel Input { get; }
    ITimeModel Time { get; }
    TModel GetModel<TModel>() where TModel : IRegisteredModel;
}
