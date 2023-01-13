using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public abstract class BehaviourState
    {
        protected Guid id;
        public BehaviourState(Guid id)
        {
            this.id = id;
        }

        public virtual void InitializeState(GameModel model)
        {
        }

        public abstract void UpdateState(GameModel model);
    }

    public abstract class BehaviourState<TStateModel> : BehaviourState
        where TStateModel : BehaviourStateModel
    {
        protected BehaviourState(Guid id) : base(id)
        {
        }

        public sealed override void InitializeState(GameModel model)
        {
            var behaviourModel = model.Behaviours.GetItem(id);
            behaviourModel.State = InitializeState(behaviourModel);
        }

        protected abstract TStateModel InitializeState(BehaviourModel behaviourModel);

        public sealed override void UpdateState(GameModel model)
        {
            var behaviourModel = model.Behaviours.GetItem(id);
            var stateModel = (TStateModel)behaviourModel.State;
            UpdateState(stateModel);
        }

        protected abstract void UpdateState(TStateModel stateModel);
    }
}