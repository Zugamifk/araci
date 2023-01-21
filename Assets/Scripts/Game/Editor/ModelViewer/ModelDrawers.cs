using System;
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

    public static void DrawSpawnModel(SpawnModel model)
    {
        EditorGUILayout.LabelField($"Id: {model.Id}");
        EditorGUILayout.LabelField($"Key: {model.Key}");
        EditorGUILayout.LabelField($"Corners: {model.BoundingCorners[0]}, {model.BoundingCorners[1]}, {model.BoundingCorners[2]}, {model.BoundingCorners[3]}");
        using (new EditorGUI.IndentLevelScope())
        {
            EditorGUILayout.LabelField("Spawn Queue", EditorStyles.boldLabel);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                foreach (var id in model.SpawnQueue)
                {
                    EditorGUILayout.LabelField(id.ToString());
                }
            }
        }
    }

    public static void DrawLevel(LevelModel model)
    {
        EditorGUILayout.LabelField($"Current Level: {model.CurrentLevel}");
        EditorGUILayout.LabelField($"Current Experience: {model.CurrentExperience}");
        EditorGUILayout.LabelField($"Last Level Required Experience: {model.LastLevelRequiredExperience}");
        EditorGUILayout.LabelField($"Required Experience: {model.RequiredExperience}");
    }

    public static void DrawSkill(SkillModel model)
    {
        EditorGUILayout.LabelField($"Key: {model.Key}");
    }

    public static void DrawWeapon(WeaponModel model)
    {
        EditorGUILayout.LabelField($"Key: {model.Key}");
        EditorGUILayout.LabelField($"Name: {model.Name}");
        EditorGUILayout.LabelField($"Description: {model.Description}");
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("Level", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                DrawLevel(model.Level);
            }
        }
    }

    public static void DrawDash(DashModel model)
    {
        EditorGUILayout.LabelField($"Duration: {model.Duration}");
        EditorGUILayout.LabelField($"Speed: {model.Speed}");
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("Cooldown", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                DrawCooldown(model.Cooldown);
            }
        }
    }

    public static void DrawInteractable(InteractableModel model)
    {
        EditorGUILayout.LabelField($"Id: {model.Id}");
        EditorGUILayout.LabelField($"Position: {model.Position}");

    }
}
