using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ModelListPane : InfoPane
{
    public ModelListPane(string tabTitle)
    {
        TabTitle = tabTitle;
    }
}

public class ModelListPane<TIdentifiable> : ModelListPane
    where TIdentifiable : IIdentifiable
{
    IIdentifiableLookup<TIdentifiable> collection;
    Vector2 scrollPosition;

    public ModelListPane(string tabTitle, IIdentifiableLookup<TIdentifiable> collection)
        : base(tabTitle)
    {
        this.collection = collection;
    }

    public override void DrawContents()
    {
        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            foreach (var item in collection.AllItems)
            {
                DrawItem(item);
            }

            scrollPosition = scrollScope.scrollPosition;
        }
    }

    void DrawItem(TIdentifiable item)
    {
        EditorGUILayout.LabelField(item.Id.ToString());
    }
}