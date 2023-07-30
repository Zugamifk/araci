using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Behaviour;
public class GameModel : IGameModel
{
    public IdentifiableCollection<PositionModel> Positions = new IdentifiableCollection<PositionModel>();
    public IdentifiableCollection<MovementModel> Movements = new IdentifiableCollection<MovementModel>();
    public IdentifiableCollection<CharacterModel> Characters { get; } = new();
    public Dictionary<string, Guid> UniqueKeyToId { get; } = new();
    public IdentifiableCollection<NarrativeModel> Narratives { get; } = new();
    public DialogModel Dialog { get; set; }
    public IdentifiableCollection<ShrineModel> Shrines { get; } = new();
    public IdentifiableCollection<HarvestableModel> Harvestables { get; } = new();
    public IdentifiableCollection<AIModel> Behaviours { get; } = new();
    public Dictionary<string, SpawnModel> Spawns { get; } = new();
    public Dictionary<string, Vector2> MapLocations { get; } = new();
    public InventoryModel Inventory { get; } = new();
    public IdentifiableCollection<ItemModel> Items { get; } = new();
    public PlayerModel Player { get; } = new();
    public CharacterModel PlayerCharacter => Characters.GetItem(Player.Id);
    public CameraModel Camera { get; } = new();
    public InputModel Input { get; } = new();
    public UIModel UI { get; } = new();
    public TimeModel TimeModel = new TimeModel();


    #region IGameModel
    ITimeModel IGameModel.Time => TimeModel;
    IInputModel IGameModel.Input => Input;

    IIdentifiableLookup<ICharacterModel> IGameModel.Characters => Characters;
    IDialogModel IGameModel.Dialog => Dialog;
    IIdentifiableLookup<IShrineModel> IGameModel.Shrines => Shrines;
    IIdentifiableLookup<IAIModel> IGameModel.Behaviours => Behaviours;
    IPlayerModel IGameModel.Player => Player;
    ICharacterModel IGameModel.PlayerCharacter => PlayerCharacter;
    IInventoryModel IGameModel.Inventory => Inventory;
    IIdentifiableLookup<IItemModel> IGameModel.Items => Items;

    IIdentifiableLookup<IPositionModel> IGameModel.Positions => Positions;

    IIdentifiableLookup<IMovementModel> IGameModel.Movements => Movements;

    IIdentifiableLookup<IHarvestableModel> IGameModel.Harvestables => Harvestables;

    IUIModel IGameModel.UI => UI;
    #endregion

}
