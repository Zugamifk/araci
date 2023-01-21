using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnModelListPane : InfoPane
{
    Dictionary<string, SpawnModel> spawns;
    Vector2 scrollPosition;
    string currentItem;

    public SpawnModelListPane(string tabTitle, Dictionary<string, SpawnModel> spawns) : base(tabTitle)
    {
        this.spawns = spawns;
    }

    public override void DrawContents()
    {
        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            foreach (var item in spawns)
            {
                DrawItem(item.Key, item.Value);
            }

            scrollPosition = scrollScope.scrollPosition;
        }
    }

    void DrawItem(string key, SpawnModel item)
    {
        bool isCurrentItem = key == currentItem;

        using (new EditorGUI.DisabledScope(isCurrentItem))
        {
            if (GUILayout.Button(key))
            {
                currentItem = key;
            }
        }

        if (isCurrentItem)
        {
            ModelDrawers.DrawSpawnModel(item);
        }
    }
}
