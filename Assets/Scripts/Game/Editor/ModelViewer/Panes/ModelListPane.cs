using System;
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

public abstract class ModelListPane<TIdentifiable> : ModelListPane
    where TIdentifiable : IIdentifiable
{
    IIdentifiableLookup<TIdentifiable> collection;
    Vector2 scrollPosition;
    Guid currentItem;

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
        bool isCurrentItem = item.Id == currentItem;

        using (new EditorGUI.DisabledScope(isCurrentItem))
        {
            if (GUILayout.Button(item.Id.ToString()))
            {
                currentItem = item.Id;
            }
        }

        if (isCurrentItem)
        {
            DrawItemData(item);
        }
    }

    protected abstract void DrawItemData(TIdentifiable item);
}