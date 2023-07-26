using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Behaviour
{
    public class WanderState : BehaviourState<WanderStateModel>
    {
        public WanderState(Guid id) : base(id)
        {
        }

        protected override WanderStateModel InitializeState(AIModel behaviourModel)
        {
            var position = Game.Model.Positions.GetItem(id);
            var wanderModel = new WanderStateModel();
            GetNewDestination(position, wanderModel);
            return wanderModel;
        }

        protected override void UpdateState(WanderStateModel stateModel)
        {
            var ms = Services.Get<ITileMapService>();

            var position = Game.Model.Positions.GetItem(id);
            //Debug.DrawLine(ms.GridToWorldSpace(character.Movement.Position), ms.GridToWorldSpace(stateModel.Destination), Color.green);
            if (position.IsApproximateAtPosition(stateModel.Destination))
            {
                GetNewDestination(position, stateModel);
            }
            else
            {
                var to = stateModel.Destination - position.Position.Value;
                Game.Do(new MoveCharacter(id, to.normalized));
            }
        }

        void GetNewDestination(IPositionModel positionModel, WanderStateModel stateModel)
        {
            var raycaster = Services.Get<IPhysicsRaycaster>();
            var pos = positionModel.Position.Value;
            var dir = UnityEngine.Random.insideUnitCircle.normalized;
            var end = raycaster.Raycast(pos, dir, 1 << LayerMask.NameToLayer(Layers.OBSTACLES));
            stateModel.Destination = Vector2.Lerp(pos, end, UnityEngine.Random.value);
        }
    }
}