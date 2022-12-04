using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnEnemy : ICommand
{
    string _characterKey;
    string _spawnKey;

    public SpawnEnemy(string characterKey, string spawnKey)
    {
        _characterKey = characterKey;
        _spawnKey = spawnKey;
    }

    public void Execute(GameModel model)
    {
        var id = Guid.NewGuid();
        new SpawnCharacter(_characterKey, _spawnKey, id).Execute(model);

        var character = model.Characters.GetItem(id);
        var data = DataService.GetData<CharacterDataCollection>().Get(_characterKey) as EnemyData;
        character.Attack.Damage = data.AttackDamage;

        Game.AddUpdater(new AgentBehaviourUpdater(id, new FrogDemonBehaviour(id)));
    }
}
