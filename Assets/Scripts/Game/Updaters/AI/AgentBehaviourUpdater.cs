using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviourUpdater : IUpdater
{
    Guid _agentId;
    AgentBehaviour _behaviour;

    public AgentBehaviourUpdater(Guid id, AgentBehaviour behaviour)
    {
        _agentId = id;
        _behaviour = behaviour;
        _behaviour.Id = id;
    }

    public void Update(GameModel model)
    {
        var agent = model.Characters.GetItem(_agentId);
        if (agent == null)
        {
            Game.RemoveUpdater(_agentId);
            return;
        }

        _behaviour.Update(model, agent);
    }
}
