using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class Character : ModelViewBase<ICharacterModel>
{
    [SerializeField]
    Transform _viewRoot;
    [SerializeField]
    Animator _animator;

    Rigidbody2D _rigidBody;
    Vector3 _lastPosition;

    public override ICharacterModel GetModel() => Game.Model.Characters.GetItem(Id);

    public override void InitializeFromModel(ICharacterModel model)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
        UpdatePosition();
    }

    void Update()
    {
        DoDesiredMove();
    }

    void UpdatePosition()
    {
        var character = GetModel();
        Map.Instance.PositionObject(character.Movement, transform);
    }

    void DoDesiredMove()
    {
        var character = GetModel();
        if(character == null)
        {
            return;
        }

        Map.Instance.MoveObject(character.Movement, _rigidBody);
        var newPosition = transform.position;

        var step = newPosition - _lastPosition;
        _lastPosition = newPosition;

        if(Mathf.Approximately(step.magnitude, 0))  
        {
            _animator.SetBool("Walking", false);
            return;
        } else
        {
            _animator.SetBool("Walking", true);
        }

        var side = step.x;
        if (!Mathf.Approximately(side, 0))
        {
            var angle = side < 0 ? 180 : 0;
            _viewRoot.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }

        Game.Do(new UpdatePosition(Id, transform.position));

    }
}
