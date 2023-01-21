using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterModelListPane : ModelListPane<CharacterModel>
{
    public CharacterModelListPane(string tabTitle, IIdentifiableLookup<CharacterModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(CharacterModel item)
    {
        using (new EditorGUI.IndentLevelScope())
        {
            EditorGUILayout.LabelField($"Key: {item.Key}");
            EditorGUILayout.LabelField($"Move Speed: {item.MoveSpeed}");

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Current Action", EditorStyles.boldLabel);
                if (item.CurrentAction != null)
                {
                    ModelDrawers.DrawAction(item.CurrentAction);
                }
                else
                {
                    GUILayout.Label("No current action");
                }
            }

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Attack", EditorStyles.boldLabel);
                ModelDrawers.DrawAttack(item.Attack);
            }

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
                ModelDrawers.DrawMovement(item.Movement);
            }

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
                ModelDrawers.DrawHealth(item.Health);
            }
        }
    }
}
