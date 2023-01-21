using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommandListPane : InfoPane
{
    Vector2 scrollPosition;

    public CommandListPane(string tabTitle) : base(tabTitle)
    {
    }

    public override void DrawContents()
    {
        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            foreach (var item in Game.EditorCommands)
            {
                DrawItem(item);
            }

            scrollPosition = scrollScope.scrollPosition;
        }
    }

    void DrawItem(ICommand command)
    {
        EditorGUILayout.LabelField(command.GetType().ToString());
    }
}
