using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    public class WanderState : BehaviourState<WanderStateModel>
    {
        const float REACH_THRESHOLD = .25f;

        public WanderState(Guid id) : base(id)
        {
        }

        protected override WanderStateModel InitializeState(AIModel behaviourModel)
        {
            var character = Game.Model.Characters.GetItem(base.id);
            var wanderModel = new WanderStateModel();
            GetNewDestination(character, wanderModel);
            return wanderModel;
        }

        protected override void UpdateState(WanderStateModel stateModel)
        {
            var ms = Services.Get<ITileMapService>();

            var character = Game.Model.Characters.GetItem(id);
            var to = stateModel.Destination - character.Movement.Position;
            Debug.DrawLine(ms.GridToWorldSpace(character.Movement.Position), ms.GridToWorldSpace(stateModel.Destination), Color.green);
            if (to.magnitude < REACH_THRESHOLD)
            {
                GetNewDestination(character, stateModel);
            }
            else
            {
                Game.Do(new MoveCharacter(id, to.normalized));
            }
        }

        void GetNewDestination(ICharacterModel characterModel, WanderStateModel stateModel)
        {
            var raycaster = Services.Get<IPhysicsRaycaster>();
            var pos = characterModel.Movement.Position;
            var dir = UnityEngine.Random.insideUnitCircle.normalized;
            var end = raycaster.Raycast(pos, dir, 1 << LayerMask.NameToLayer(Layers.OBSTACLES));
            stateModel.Destination = Vector2.Lerp(pos, end, UnityEngine.Random.value);
        }
    }
}