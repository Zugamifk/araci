using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public abstract class AgentBehaviour
    {
        protected Guid id;
        protected BehaviourState currentState { get; set; }

        public AgentBehaviour(Guid id)
        {
            this.id = id;
            currentState = new IdleState(id);
        }

        public abstract void Initialize(GameModel model);

        public abstract void Update(GameModel model);
    }

    public abstract class AgentBehaviour<TAgentModel> : AgentBehaviour
        where TAgentModel : AgentModel
    {
        protected AgentBehaviour(Guid id) : base(id)
        {
        }

        public sealed override void Initialize(GameModel model)
        {
            EnterState(model, currentState);
        }

        public sealed override void Update(GameModel model)
        {
            var behaviour = model.Behaviours.GetItem(id);
            var agent = (TAgentModel)behaviour.Agent;
            currentState.Update(model);

            if (currentState.CanTransition)
            {
                var newState = TransitionState(behaviour, agent);
                if (newState != null)
                {
                    EnterState(model, newState);
                }
            }
        }

        void EnterState(GameModel model, BehaviourState state)
        {
            state.Initialize(model);
            currentState = state;
        }

        protected abstract BehaviourState TransitionState(AIModel behaviour, TAgentModel agent);
    }
}