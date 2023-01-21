using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class TimeModelPane : InfoPane
{
    Vector2 scrollPosition;

    public TimeModelPane(string tabTitle) : base(tabTitle)
    {
    }

    public override void DrawContents()
    {
        var time = Game.EditorModel.TimeModel;

        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            GUILayout.Label($"Time Multiplier: {TimeModel.TIME_MULTIPLIER}");
            GUILayout.Label($"Real Time: {time.RealTime}");
            GUILayout.Label($"Time: {time.Time}");
            GUILayout.Label($"Clock Time: {time.Hour}:{time.Minute}");
            scrollPosition = scrollScope.scrollPosition;
        }
    }
}
