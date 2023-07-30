using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameModel
{
    IIdentifiableLookup<IPositionModel> Positions { get; }
    IIdentifiableLookup<IMovementModel> Movements { get; }
    IIdentifiableLookup<ICharacterModel> Characters { get; }
    IIdentifiableLookup<IShrineModel> Shrines { get; }
    IIdentifiableLookup<IHarvestableModel> Harvestables { get; }
    IIdentifiableLookup<IAIModel> Behaviours { get; }
    IIdentifiableLookup<IItemModel> Items { get; }
    IInventoryModel Inventory { get; }
    IDialogModel Dialog { get; }
    IPlayerModel Player { get; }
    ICharacterModel PlayerCharacter { get; }
    IInputModel Input { get; }
    ITimeModel Time { get; }
}
