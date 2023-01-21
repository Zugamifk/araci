using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ModelDrawers
{
    public static void DrawAction(IActionModel action)
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

    public static void DrawCooldown(ICooldownModel model)
    {
        EditorGUILayout.LabelField($"Ready Time: {model.ReadyTime}");
    }

    public static void DrawAnimationState(IAnimationStateModel model)
    {
        EditorGUILayout.LabelField($"Key: {model.Key}");
    }

    public static void DrawMovement(IMovementModel model)
    {
        EditorGUILayout.LabelField($"Speed: {model.Speed}");
        EditorGUILayout.LabelField($"Position: {model.Position}");
        EditorGUILayout.LabelField($"Direction: {model.Direction}");
    }

    public static void DrawHealth(IHealthModel model)
    {
        EditorGUILayout.LabelField($"CurrentHealth: {model.CurrentHealth}");
        EditorGUILayout.LabelField($"MaxHealth: {model.MaxHealth}");
        EditorGUILayout.LabelField($"IsAlive: {model.IsAlive}");
    }
}
