using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    Collider2D _collider;

    [SerializeField]
    DashEffect _dash;

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

    void Update()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }

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

    private void LateUpdate()
    {
        Game.Do(new SetCharacterPosition(Id, Map.Instance.WorldToGridSpace(transform.position)));
        UpdatePosition();
    }

    void UpdatePosition()
    {
        var character = GetModel();
        Map.Instance.PositionObject(character.Movement, transform);
    }

    void DoDesiredMove(ICharacterModel character)
    {
        Map.Instance.MoveObject(character.Movement, _rigidBody);

        var move = _rigidBody.velocity;
        if (Mathf.Approximately(move.sqrMagnitude, 0))
        {
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
            case Actions.DASH:
                _collider.enabled = false;
                _dash?.DoDash();
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

        var side = dir.x;
        if (!Mathf.Approximately(side, 0))
        {
            var angle = side < 0 ? 180 : 0;
            _viewRoot.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
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
