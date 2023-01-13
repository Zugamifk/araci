using StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : EnemyBehaviourState
{
    const float REACH_THRESHOLD = .25f;
    Vector2 _destination;
    public WanderState(Guid id) : base(id)
    {
        var character = Game.Model.Characters.GetItem(base.id);
        GetNewDestination(character);
    }

    public override IState UpdateState()
    {
        var ms = Services.Get<ITileMapService>();

        var character = Game.Model.Characters.GetItem(id);
        var to = _destination - character.Movement.Position;
        Debug.DrawLine(ms.GridToWorldSpace(character.Movement.Position), ms.GridToWorldSpace(_destination), Color.green);
        if (to.magnitude < REACH_THRESHOLD)
        {
            GetNewDestination(character);
        } else
        {
            Game.Do(new MoveCharacter(id, to.normalized));
        }
        return this;
    }

    void GetNewDestination(ICharacterModel model)
    {
        var raycaster = Services.Get<IPhysicsRaycaster>();
        var pos = model.Movement.Position;
        var dir = UnityEngine.Random.insideUnitCircle.normalized;
        var end = raycaster.Raycast(pos, dir, 1 << LayerMask.NameToLayer(Layers.OBSTACLES));
        _destination = Vector2.Lerp(pos, end, UnityEngine.Random.value);
    }
}
