using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Identifiable))]
[RequireComponent(typeof(Character))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool hasWalkAnimation;

    private void Awake()
    {
        var id = GetComponent<Identifiable>();
        id.IdChanged += OnIdSet;
    }

    void OnIdSet(Guid id)
    {
        if(id == Guid.Empty)
        {
            return;
        }

        var movement = Game.Model.Movements[id];
        movement.Speed.ValueChanged += OnSpeedChanged;

        var character = Game.Model.Characters[id];
        character.CurrentAction.ValueChanged += OnActionChanged;
    }

    void OnSpeedChanged(float _, float value)
    {
        var isWalking = value > 0;
        animator.SetBool(Animation.WALK, isWalking);
    }

    void OnActionChanged(IActionModel oldAction, IActionModel newAction)
    {
        if(newAction == null)
        {
            return;
        }

        var specialAnim = newAction.AnimationState;
        if (!string.IsNullOrEmpty(specialAnim.Key))
        {
            animator.SetTrigger(specialAnim.Key);
        }
    }
}
