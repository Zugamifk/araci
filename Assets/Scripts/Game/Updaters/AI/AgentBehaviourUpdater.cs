using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class AgentBehaviourUpdater : IUpdater
    {
        Guid agentId;
        AgentBehaviour behaviour;
        bool initialized;

        public AgentBehaviourUpdater(Guid id, AgentBehaviour behaviour)
        {
            agentId = id;
            this.behaviour = behaviour;
            initialized = false;
        }

        public void Update(GameModel model)
        {
            var agent = model.Characters.GetItem(agentId);
            if (agent == null)
            {
                Game.RemoveUpdater(agentId);
                return;
            }

            if(!initialized)
            {
                behaviour.Initialize(model);
                initialized = true;
            }

            behaviour.Update(model);
        }
    }
}