using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class Character : MonoBehaviour, IModelView<ICharacterModel>
{
    [SerializeField]
    Transform _viewRoot;
    [SerializeField]
    Animator _animator;

    public Guid Id => _identifiable.Id;
    Identifiable _identifiable;
    Rigidbody2D _rigidBody;
    Vector3 _lastPosition;

    public ICharacterModel GetModel() => Game.Model.Characters.GetItem(_identifiable.Id);

    public void InitializeFromModel(ICharacterModel model)
    {
        _identifiable = GetComponent<Identifiable>();
        _identifiable.Id = model.Id;
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
        var movement = Game.Model.Movement.GetItem(_identifiable.Id);
        Map.Instance.PositionObject(movement, transform);
    }

    void DoDesiredMove()
    {
        var movement = Game.Model.Movement.GetItem(_identifiable.Id);
        Map.Instance.MoveObject(movement, _rigidBody);
        var newPosition = transform.position;

        var step = newPosition - _lastPosition;
        _lastPosition = newPosition;

        Debug.Log($"{this} {movement.DesiredMove} {movement.Position} {step.x} {Mathf.Approximately(step.magnitude, 0)}");
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

        Game.Do(new UpdatePosition(_identifiable.Id, transform.position));

    }
}
