using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Identifiable))]
[RequireComponent (typeof(Character))]
public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    Identifiable identifiable;
    Guid lastActionId;

    private void Awake()
    {
        identifiable = GetComponent<Identifiable>();
    }

    private void Update()
    {
        var character = Game.Model.Characters.GetItem(identifiable.Id);

        animator.SetBool(Animation.WALK, character.Movement.Mode != MoveMode.None);

        if(lastActionId!=character.CurrentAction.Id)
        {
            DoActionAnimation(character);
        }
    }

    void DoActionAnimation(ICharacterModel character)
    {
        var specialAnim = character.CurrentAction.AnimationState;
        if (!string.IsNullOrEmpty(specialAnim.Key))
        {
            animator.SetTrigger(specialAnim.Key);
        }
        lastActionId = character.CurrentAction.Id;
    }
}
