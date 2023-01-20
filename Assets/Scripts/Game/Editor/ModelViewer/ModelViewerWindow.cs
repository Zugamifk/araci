using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class ModelViewerWindow : EditorWindow
{
    [MenuItem("Window/Model Viewer")]
    static void Open()
    {
        ModelViewerWindow window = CreateInstance<ModelViewerWindow>();
        window.Show();
    }

    InfoPane[] infoPanes;
    int currentPaneIndex = 0;

    void LoadModel()
    {
        var game = Game.Model;
        infoPanes = new InfoPane[]
        {
            new ModelListPane<ICharacterModel>("Characters", game.Characters),
            new ModelListPane<IShrineModel>("Shrines", game.Shrines),
        };
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
        if (infoPanes == null)
        {
            LoadModel();
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            DrawTabs();

            infoPanes[currentPaneIndex].DrawContents();
        }
    }

    void DrawTabs()
    {
        using(new EditorGUILayout.VerticalScope("box"))
        {
            for(int i=0;i<infoPanes.Length;i++)
            {
                using (new EditorGUI.DisabledScope(i == currentPaneIndex))
                {
                    if (GUILayout.Button(infoPanes[i].TabTitle, GUILayout.Width(100)))
                    {
                        currentPaneIndex = i;
                    }
                }
            }
        }
    }
}
