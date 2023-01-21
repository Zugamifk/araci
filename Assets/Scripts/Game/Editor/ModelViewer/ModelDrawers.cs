using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ModelDrawers
{
    public static void DrawAction(ActionModel action)
    {
        EditorGUILayout.LabelField($"ID: {action.Id}");
        EditorGUILayout.LabelField($"Key: {action.Key}");
        EditorGUILayout.LabelField($"Target Position: {action.TargetPosition}");
        using (new EditorGUI.IndentLevelScope())
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Cooldown", EditorStyles.boldLabel);
                DrawCooldown(action.Cooldown);
            }
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Animation State", EditorStyles.boldLabel);
                DrawAnimationState(action.AnimationState);
            }
        }
    }

    public static void DrawCooldown(CooldownModel model)
    {
        EditorGUILayout.LabelField($"Ready Time: {model.ReadyTime}");
        EditorGUILayout.LabelField($"Duration: {model.Duration}");
        EditorGUILayout.LabelField($"Cooldown: {model.Cooldown}");
    }

    public static void DrawAnimationState(AnimationStateModel model)
    {
        EditorGUILayout.LabelField($"Key: {model.Key}");
    }

    public static void DrawMovement(MovementModel model)
    {
        EditorGUILayout.LabelField($"Mode: {model.Mode}");
        EditorGUILayout.LabelField($"Speed: {model.Speed}");
        EditorGUILayout.LabelField($"Position: {model.Position}");
        EditorGUILayout.LabelField($"Direction: {model.Direction}");
        EditorGUILayout.LabelField($"Destination: {model.Destination}");
    }

    public static void DrawHealth(HealthModel model)
    {
        EditorGUILayout.LabelField($"Current Health: {model.CurrentHealth}");
        EditorGUILayout.LabelField($"Max Health: {model.MaxHealth}");
        EditorGUILayout.LabelField($"Is Alive: {model.IsAlive}");
    }

    public static void DrawAttack(AttackModel model)
    {
        EditorGUILayout.LabelField($"Damage: {model.Damage}");
        EditorGUILayout.LabelField($"Cooldown: {model.Cooldown}");
    }
}
