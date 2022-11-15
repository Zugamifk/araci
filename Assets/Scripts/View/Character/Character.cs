using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Identifiable))]
public class Character : MonoBehaviour, IModelView<ICharacterModel>
{
    Identifiable _identifiable;

    public ICharacterModel GetModel() => Game.Model.Characters.GetItem(_identifiable.Id);

    public void InitializeFromModel(ICharacterModel model)
    {
        _identifiable = GetComponent<Identifiable>();
        _identifiable.Id = model.Id;
    }

    void Update()
    {
        var movement = Game.Model.Movement.GetItem(_identifiable.Id);
        transform.position = movement.Position;
    }
}
