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

    Identifiable identifiable;
    Guid lastActionId;

    private void Awake()
    {
        identifiable = GetComponent<Identifiable>();
    }

    private void Update()
    {
        var character = Game.Model.Characters.GetItem(identifiable.Id);
        if (character == null)
        {
            return;
        }

        DoWalkAnimation(character);
        DoActionAnimation(character);
    }

    void DoWalkAnimation(ICharacterModel character)
    {
        if (!hasWalkAnimation)
        {
            return;
        }

        var movement = Game.Model.Movements.GetItem(lastActionId);
        var isWalking = movement.Speed > 0;
        animator.SetBool(Animation.WALK, isWalking);
    }

    void DoActionAnimation(ICharacterModel character)
    {
        if (lastActionId == character.CurrentAction.Id)
        {
            return;
        }

        var specialAnim = character.CurrentAction.AnimationState;
        if (!string.IsNullOrEmpty(specialAnim.Key))
        {
            animator.SetTrigger(specialAnim.Key);
        }

        lastActionId = character.CurrentAction.Id;
    }
}
