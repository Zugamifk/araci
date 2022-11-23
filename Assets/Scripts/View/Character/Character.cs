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
    [SerializeField]
    DelayedAnimationEffect _deathEffect;

    Rigidbody2D _rigidBody;

    public override ICharacterModel GetModel() => Game.Model.Characters.GetItem(Id);

    public override void InitializeFromModel(ICharacterModel model)
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        UpdatePosition();
    }

    void Update()
    {
        var character = GetModel();
        if(character == null)
        {
            return;
        } else if (character.Health.IsAlive)
        {
            DoDesiredMove(character);
        } else
        {
            Die();
        }
    }

    void UpdatePosition()
    {
        var character = GetModel();
        Map.Instance.PositionObject(character.Movement, transform);
    }

    void DoDesiredMove(ICharacterModel character)
    {
        if (character == null)
        {
            return;
        }

        Map.Instance.MoveObject(character.Movement, _rigidBody);

        var move = _rigidBody.velocity;
        if (move.magnitude > 0)
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
            return;
        }

        var side = move.x;
        if (!Mathf.Approximately(side, 0))
        {
            var angle = side < 0 ? 180 : 0;
            _viewRoot.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }

        Game.Do(new UpdatePosition(Id, transform.position));

    }

    void Die()
    {
        _deathEffect.Play();
        Game.Do(new RemoveCharacter(Id));
    }
}
