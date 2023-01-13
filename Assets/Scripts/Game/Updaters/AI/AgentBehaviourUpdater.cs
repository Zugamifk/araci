using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class AgentBehaviourUpdater : IUpdater
    {
        Guid _agentId;
        AgentBehaviour _behaviour;

        public AgentBehaviourUpdater(Guid id, AgentBehaviour behaviour)
        {
            _agentId = id;
            _behaviour = behaviour;
        }

        public void Update(GameModel model)
        {
            var agent = model.Characters.GetItem(_agentId);
            if (agent == null)
            {
                Game.RemoveUpdater(_agentId);
                return;
            }

            _behaviour.Update(model);
        }
    }
}