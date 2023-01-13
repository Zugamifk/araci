using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behaviour;

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

        var behaviour = GetBehaviour(id);
        Game.AddUpdater(new AgentBehaviourUpdater(id, behaviour));
    }

    AgentBehaviour GetBehaviour(Guid id)
    {
        switch (_characterKey)
        {
            case Enemies.FROGDEMON:
                return new FrogDemonBehaviour(id);
            //case Enemies.PIPER:
            //    return new PiperBehaviour(id);
            default:
                throw new ArgumentException($"No behaviour for key \'{_characterKey}\' with ID {id}");
        }
    }
}
