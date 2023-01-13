using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public abstract class AgentBehaviour
    {
        protected Guid id;
        public AgentBehaviour(Guid id)
        {
            this.id = id;
        }

        public abstract void Update(GameModel model);
    }

    public abstract class AgentBehaviour<TAgentModel> : AgentBehaviour
        where TAgentModel : AgentModel
    {
        protected AgentBehaviour(Guid id) : base(id)
        {
        }

        public sealed override void Update(GameModel model)
        {
            var behaviour = model.Behaviours.GetItem(id);
            var agent = (TAgentModel)behaviour.Agent;
            Update(behaviour, agent);
        }

        protected abstract void Update(BehaviourModel behaviour, TAgentModel agent);
    }
}