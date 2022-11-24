using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InitializePlayer : ICommand
{
    public void Execute(GameModel model)
    {
        var player = model.Player;
        new CreateCharacter(player.Id, "Player", new Vector2(5, 10)).Execute(model);

        var level = player.Level;
        level.CurrentLevel = 0;
        var data = DataService.GetData<LevelDataCollection>().GetLevel(0);
        level.RequiredExperience = data.RequiredExperience;

        var weaponData = DataService.GetData<WeaponDataCollection>().Get(Weapons.SWORD);
        var weapon = player.Weapon;
        weapon.Level.CurrentLevel = 0;
        var weaponLevelData = weaponData.LevelData.GetLevel(0);
        weapon.Level.RequiredExperience = weaponLevelData.RequiredExperience;
        weapon.AttackCooldown.ReadyTime = 0;
        weapon.AttackCooldown.Cooldown = weaponData.BaseAttackTime;
    }
}
