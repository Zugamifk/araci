using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Behaviour
{
    public class IdleState : BehaviourState
    {
        public IdleState(Guid id) : base(id)
        {
            CanTransition = true;
        }

        public override void Initialize(GameModel model)
        {
            Game.Do(new StopCharacter(id));
        }

        public override void Update(GameModel gameModel)
        {
            
        }
    }
}