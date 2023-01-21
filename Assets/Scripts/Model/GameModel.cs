using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GameModel : IGameModel
{
    public IdentifiableCollection<CharacterModel> Characters { get; } = new();
    public IdentifiableCollection<NarrativeModel> Narratives { get; } = new();
    public IdentifiableCollection<ShrineModel> Shrines { get; } = new();
    public Dictionary<string, SpawnModel> Spawns { get; } = new();
    public PlayerModel Player { get; } = new();
    public CharacterModel PlayerCharacter => Characters.GetItem(Player.Id);
    public InputModel Input { get; } = new();
    public TimeModel TimeModel = new TimeModel();

    #region IGameModel
    ITimeModel IGameModel.Time => TimeModel;
    IInputModel IGameModel.Input => Input;

    IIdentifiableLookup<ICharacterModel> IGameModel.Characters => Characters;
    IIdentifiableLookup<IShrineModel> IGameModel.Shrines => Shrines;
    IPlayerModel IGameModel.Player => Player;
    ICharacterModel IGameModel.PlayerCharacter => PlayerCharacter;
    #endregion

}
