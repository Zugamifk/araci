using Behaviour;
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
            EditorGUILayout.LabelField("Animation State", EditorStyles.boldLabel);
            DrawAnimationState(action.AnimationState);
        }
    }

    public static void DrawCooldown(CooldownModel model)
    {
        EditorGUILayout.LabelField($"Ready Time: {model.ReadyTime}");
        EditorGUILayout.LabelField($"Duration: {model.Duration}");
    }

    public static void DrawAnimationState(AnimationStateModel model)
    {
        EditorGUILayout.LabelField($"Key: {model.Key}");
    }

    public static void DrawPosition(PositionModel model)
    {
        EditorGUILayout.LabelField($"Position: {model.Position.Value}");
    }

    public static void DrawMovement(MovementModel model)
    {
        EditorGUILayout.LabelField($"Speed: {model.Speed.Value}");
        EditorGUILayout.LabelField($"Direction: {model.Direction.Value}");
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
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("Cooldown", EditorStyles.boldLabel);
            DrawCooldown(model.Cooldown);
        }
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
            DrawCooldown(model.Cooldown);
        }
    }

    public static void DrawInteractable(InteractableModel model)
    {
        EditorGUILayout.LabelField($"Id: {model.Id}");
        EditorGUILayout.LabelField($"Position: {model.Position}");

    }
    public static void DrawAgent(AgentModel model)
    {
        if (model is FrogDemonAgentModel frogDemon)
        {
            EditorGUILayout.LabelField("Frog Demon", EditorStyles.boldLabel);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Jump Cooldown", EditorStyles.boldLabel);
                DrawCooldown(frogDemon.JumpCooldown);
            }
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Attack Cooldown", EditorStyles.boldLabel);
                DrawCooldown(frogDemon.AttackCooldown);
            }
        }
        else if (model is PiperAgentModel piperAgent)
        {
            EditorGUILayout.LabelField("Piper", EditorStyles.boldLabel);
            // nothing for now
        }
    }

    public static void DrawBehaviourState(BehaviourStateModel model)
    {
        if (model is WanderStateModel wander)
        {
            EditorGUILayout.LabelField("Wander", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Destination: {wander.Destination}");
        }
        else if (model is AttackStateModel attack)
        {
            EditorGUILayout.LabelField("Attack", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Direction: {attack.Direction}");
            EditorGUILayout.LabelField($"EndTime: {attack.EndTime}");
        }
        else if (model is JumpStateModel jump)
        {
            EditorGUILayout.LabelField("Jump", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Direction: {jump.Direction}");
            EditorGUILayout.LabelField($"Speed: {jump.Speed}");
            EditorGUILayout.LabelField($"EndTime: {jump.EndTime}");
        }
    }
}
