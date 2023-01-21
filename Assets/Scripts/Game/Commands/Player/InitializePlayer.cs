using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InitializePlayer : ICommand
{
    public void Execute(GameModel model)
    {
        var player = model.Player;
        new CreateCharacter(player.Id, "Player", new Vector2(5, 10)).Execute(model);
        var character = model.Characters.GetItem(player.Id);

        var level = player.Level;
        level.CurrentLevel = 0;
        var data = DataService.GetData<LevelDataCollection>().GetLevel(0);
        level.RequiredExperience = data.RequiredExperience;

        var weaponData = DataService.GetData<WeaponDataCollection>().Get(Weapons.SWORD);
        var weapon = player.Weapon;
        weapon.Level.CurrentLevel = 0;
        var weaponLevelData = weaponData.LevelData.GetLevel(0);
        weapon.Level.RequiredExperience = weaponLevelData.RequiredExperience;
        character.Attack.Cooldown.Duration = weaponData.BaseAttackTime;
        character.Attack.Damage = weaponData.BaseDamage;

        var playerData = DataService.GetData<PlayerData>();
        var dash = player.Dash;
        dash.Cooldown.Duration = playerData.DashCooldown;
        dash.Speed = playerData.DashSpeed;
        dash.Duration = playerData.DashDuration;
    }

}
