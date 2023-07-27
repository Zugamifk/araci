using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(IIdentifiable))]
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

    public override ICharacterModel GetModel() => Game.Model.Characters.GetItem(Id);

    public override void InitializeFromModel(ICharacterModel model)
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (_attack != null)
        {
            _attack.OnUpdatedAttackTargets += OnUpdatedAttackTargets;
        }

        var pos = Game.Model.Positions[model.Id];
        pos.Position.ValueChanged += OnPositionChanged;

        model.Health.IsAlive.ValueChanged += OnDeath;
        model.CurrentAction.ValueChanged += OnActionChanged;
    }

    void OnDeath(bool wasAlive, bool isAlive)
    {
        if (wasAlive && !isAlive)
        {
            Die();
        }
    }

    void OnActionChanged(IActionModel _, IActionModel action)
    {
        if(action == null)
        {
            return;
        }

        switch (action.Key)
        {
            case Actions.ATTACK:
                Attack(action);
                break;
            case Actions.DASH:
                _collider.enabled = false;
                _dash?.DoDash();
                break;
            default:
                break;
        }
    }
    void Die()
    {
        _deathEffect.Play();
        Game.Do(new RemoveCharacter(Id));
    }

    void Attack(IActionModel model)
    {
        _attack.gameObject.SetActive(true);

        var dir = model.TargetPosition - (Vector2)_attack.transform.position;
        _attack.transform.rotation = Math.PointAt(dir.normalized);

        var side = dir.x;
        if (!Mathf.Approximately(side, 0))
        {
            var angle = side < 0 ? 180 : 0;
            _viewRoot.transform.localRotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void FixedUpdate()
    {
        var character = GetModel();
        if (character == null)
        {
            return;
        }
        else if (character.Health.IsAlive.Value)
        {
            DoDesiredMove(character);
        }
    }

    void OnPositionChanged(Vector2 oldPosition, Vector2 newPosition)
    {
        Map.Instance.PositionObject(newPosition, transform);
    }

    void DoDesiredMove(ICharacterModel character)
    {
        var movement = Game.Model.Movements.GetItem(Id);
        Map.Instance.MoveObject(movement, _rigidBody);

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

    
    void OnUpdatedAttackTargets(IEnumerable<Guid> targets)
    {
        Game.Do(new ApplyAttackEffect(Id, new List<Guid>(targets)));
    }
}
