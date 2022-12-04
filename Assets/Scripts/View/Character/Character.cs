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
    [SerializeField]
    Attack _attack;

    Rigidbody2D _rigidBody;
    Guid _lastActionId;

    public override ICharacterModel GetModel() => Game.Model.Characters.GetItem(Id);

    public override void InitializeFromModel(ICharacterModel model)
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (_attack != null)
        {
            _attack.OnUpdatedAttackTargets += OnUpdatedAttackTargets;
        }

        UpdatePosition();
    }

    void UpdateCrouch(KeyCode crouchKey)
    {
        if (Input.GetKeyDown(crouchKey))
        {
            StartCoroutine(Crouch(true));
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            StartCoroutine(Crouch(false));
        }
    }

    IEnumerator Crouch(bool down)
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            var h = Mathf.Lerp(.5f, 1, down ? 1 - t : t);
            transform.localScale = new Vector3(1, h, 1);
            yield return null;
        }
    }

    void Update()
    {
        var character = GetModel();
        if (character == null)
        {
            return;
        }
        else if (character.Health.IsAlive)
        {
            DoDesiredMove(character);
            UpdateAction(character);
        }
        else
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

        Game.Do(new UpdatePosition(Id, transform.position));

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
    }

    void UpdateAction(ICharacterModel model)
    {
        var action = model.CurrentAction;
        if (action.Id == _lastActionId)
        {
            return;
        }

        switch (action.Key)
        {
            case Actions.ATTACK:
                Attack(model);
                break;
            default:
                break;
        }

        var specialAnim = action.AnimationState;
        switch (specialAnim.Key)
        {
            case Animation.JUMP:
                _animator.SetTrigger(Animation.JUMP);
                break;
            default:
                break;
        }

        _lastActionId = action.Id;
    }

    void Attack(ICharacterModel model)
    {
        _attack.gameObject.SetActive(true);

        var dir = model.CurrentAction.TargetPosition - (Vector2)_attack.transform.position;
        _attack.transform.rotation = Math.PointAt(dir.normalized);
    }

    void Die()
    {
        _deathEffect.Play();
        Game.Do(new RemoveCharacter(Id));
    }

    void OnUpdatedAttackTargets(IEnumerable<Guid> targets)
    {
        Game.Do(new ApplyAttackEffect(Id, new List<Guid>(targets)));
    }
}
