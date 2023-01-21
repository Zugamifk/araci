using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerModelPane : InfoPane
{
    Vector2 scrollPosition;

    public PlayerModelPane(string tabTitle) : base(tabTitle)
    {
    }

    public override void DrawContents()
    {
        var player = Game.EditorModel.Player;

        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            using (new EditorGUILayout.VerticalScope())
            {
                GUILayout.Label($"Id: {player.Id}");

                using (new EditorGUILayout.VerticalScope("box"))
                {
                    EditorGUILayout.LabelField("Level", EditorStyles.boldLabel);
                    ModelDrawers.DrawLevel(player.Level);
                }

                using (new EditorGUILayout.VerticalScope("box"))
                {
                    EditorGUILayout.LabelField("Weapon", EditorStyles.boldLabel);
                    ModelDrawers.DrawWeapon(player.Weapon);
                }

                using (new EditorGUILayout.VerticalScope("box"))
                {
                    EditorGUILayout.LabelField("Dash", EditorStyles.boldLabel);
                    ModelDrawers.DrawDash(player.Dash);
                }
            }
            scrollPosition = scrollScope.scrollPosition;
        }
    }
}
