using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GameModel : IGameModel
{
    public IdentifiableCollection<MovementModel> Movement { get; } = new();
    public IdentifiableCollection<CharacterModel> Characters { get; } = new();
    public Dictionary<string, SpawnModel> Spawns { get; } = new();
    public PlayerModel Player { get; } = new();
    public InputModel Input { get; } = new();
    public TimeModel TimeModel = new TimeModel();
    public Dictionary<Type, object> TypeToModel = new();

    public TModel GetModel<TModel>()
        where TModel : IRegisteredModel
    {
        if (TypeToModel.TryGetValue(typeof(TModel), out object model))
        {
            return (TModel)model;
        }
        else
        {
            return default;
        }
    }

    public TModel CreateModel<TModel>()
        where TModel : IRegisteredModel, new()
    {
        var result = new TModel();
        SetModel(result);
        return result;
    }

    public void SetModel<TModel>(TModel model)
        where TModel : IRegisteredModel
    {
        TypeToModel[typeof(TModel)] = model;
        foreach (var i in typeof(TModel).GetInterfaces())
        {
            if (typeof(IRegisteredModel).IsAssignableFrom(i))
            {
                TypeToModel[i] = model;
            }
        }
    }

    #region IGameModel
    ITimeModel IGameModel.Time => TimeModel;
    IInputModel IGameModel.Input => Input;

    IIdentifiableLookup<IMovementModel> IGameModel.Movement => Movement;

    IIdentifiableLookup<ICharacterModel> IGameModel.Characters => Characters;

    IPlayerModel IGameModel.Player => Player;
    #endregion

}
