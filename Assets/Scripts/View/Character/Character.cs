using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class Character : MonoBehaviour, IModelView<ICharacterModel>
{
    [SerializeField]
    Transform _viewRoot;

    public Guid Id => _identifiable.Id;
    Identifiable _identifiable;

    public ICharacterModel GetModel() => Game.Model.Characters.GetItem(_identifiable.Id);

    public void InitializeFromModel(ICharacterModel model)
    {
        _identifiable = GetComponent<Identifiable>();
        _identifiable.Id = model.Id;
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
        var lastPosition = transform.position;
        Map.Instance.MoveObject(movement, transform);
        var newPosition = transform.position;
        var side = (newPosition - lastPosition).x;
        var angle = side < 0 ? 180 : 0;
        _viewRoot.transform.localRotation = Quaternion.Euler(0, angle, 0);

        Game.Do(new UpdatePosition(_identifiable.Id, transform.position));
    }
}
