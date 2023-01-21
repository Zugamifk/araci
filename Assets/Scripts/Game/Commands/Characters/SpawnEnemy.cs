using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behaviour;
using UnityEngine.EventSystems;

public struct SpawnEnemy : ICommand
{
    string characterKey;
    string spawnKey;

    public SpawnEnemy(string characterKey, string spawnKey)
    {
        this.characterKey = characterKey;
        this.spawnKey = spawnKey;
    }

    public void Execute(GameModel model)
    {
        var id = Guid.NewGuid();
        CreateEnemyCharacter(model, id);
        CreateBehaviourModel(model, id);
        CreateBehaviour(id);
    }

    void CreateEnemyCharacter(GameModel model, Guid id)
    {
        new SpawnCharacter(characterKey, spawnKey, id).Execute(model);

        var character = model.Characters.GetItem(id);
        var data = DataService.GetData<CharacterDataCollection>().Get(characterKey) as EnemyData;
        character.Attack.Damage = data.AttackDamage;
        character.Attack.Range = data.AttackRange;
    }

    void CreateBehaviourModel(GameModel model, Guid id)
    {
        AIModel aiModel = new AIModel()
        {
            Id = id
        };
        switch (characterKey)
        {
            case Enemies.FROGDEMON:
                {
                    var agent = new FrogDemonAgentModel();
                    agent.JumpCooldown.Duration = 3;
                    agent.JumpCooldown.ReadyTime = model.TimeModel.Time + UnityEngine.Random.value;
                    agent.AttackCooldown.Duration = 1;
                    aiModel.Agent = agent;
                }
                break;
            case Enemies.PIPER:
                aiModel.Agent = new PiperAgentModel();
                break;
            default:
                throw new ArgumentException($"No model for key \'{characterKey}\' with ID {id}");
        }
        model.Behaviours.AddItem(aiModel);
    }

    void CreateBehaviour(Guid id)
    {
        AgentBehaviour behaviour = null;
        switch (characterKey)
        {
            case Enemies.FROGDEMON:
                behaviour =  new FrogDemonBehaviour(id);
                break;
            case Enemies.PIPER:
                behaviour = new PiperBehaviour(id);
                break;
            default:
                throw new ArgumentException($"No behaviour for key \'{characterKey}\' with ID {id}");
        }
        Game.AddUpdater(new AgentBehaviourUpdater(id, behaviour));
    }
}
