using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModelViewerWindow : EditorWindow
{
    [MenuItem("Window/Model Viewer")]
    static void Open()
    {
        ModelViewerWindow window = CreateInstance<ModelViewerWindow>();
        window.Show();
    }

    string[] tabs;
    Dictionary<string, InfoPane> infoPanes;

    private void OnEnable()
    {
        if(!Application.isPlaying)
        {
            return;
        }

        LoadModel();
    }
    void LoadModel()
    {
        var game = Game.Model;
        infoPanes.Add("Characters", new ModelListPane());
    }

    private void OnGUI()
    {
        if (Application.isPlaying)
        {
            DrawMainUI();
        } else
        {
            DrawEditorModeMessage();
        }
    }

    void DrawEditorModeMessage()
    {
        EditorGUILayout.HelpBox("Enter play mode to use this window", MessageType.Info);
    }

    void DrawMainUI()
    {
        DrawTabs();
    }

    void DrawTabs()
    {

    }
}
