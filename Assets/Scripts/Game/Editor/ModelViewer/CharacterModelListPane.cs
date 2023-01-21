using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterModelListPane : ModelListPane<ICharacterModel>
{
    public CharacterModelListPane(string tabTitle, IIdentifiableLookup<ICharacterModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(ICharacterModel item)
    {
        using (new EditorGUI.IndentLevelScope())
        {
            EditorGUILayout.LabelField($"Key: {item.Key}");

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
