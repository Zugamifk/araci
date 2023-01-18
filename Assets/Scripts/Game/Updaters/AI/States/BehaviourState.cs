using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public abstract class BehaviourState
    {
        protected Guid id;
        public bool CanTransition { get; set; } = false;
        public BehaviourState(Guid id)
        {
            this.id = id;
        }

        public virtual void Initialize(GameModel model)
        {
        }

        public abstract void Update(GameModel model);
    }

    public abstract class BehaviourState<TStateModel> : BehaviourState
        where TStateModel : BehaviourStateModel
    {
        protected BehaviourState(Guid id) : base(id)
        {
        }

        public sealed override void Initialize(GameModel model)
        {
            var behaviourModel = model.Behaviours.GetItem(id);
            behaviourModel.State = InitializeState(behaviourModel);
        }

        protected abstract TStateModel InitializeState(AIModel behaviourModel);

        public sealed override void Update(GameModel model)
        {
            var behaviourModel = model.Behaviours.GetItem(id);
            var stateModel = (TStateModel)behaviourModel.State;
            UpdateState(stateModel);
        }

        protected abstract void UpdateState(TStateModel stateModel);
    }
}