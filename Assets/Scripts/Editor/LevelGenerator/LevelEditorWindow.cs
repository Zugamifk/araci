using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("Window/Level Editor")]
    public static void ShowExample()
    {
        LevelEditorWindow wnd = GetWindow<LevelEditorWindow>();
        wnd.titleContent = new GUIContent("Level Editor");
    }

    List<Button> m_Buttons = new List<Button>();

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/LevelGenerator/LevelEditorWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        m_Buttons.Clear();

        var tilesbutton = root.Q<ToolbarButton>("tiles");
        tilesbutton.clicked += () => ClickedToolbarButton(0);
        m_Buttons.Add(tilesbutton);

        var levelbutton = root.Q<ToolbarButton>("level");
        levelbutton.clicked += () => ClickedToolbarButton(1);
        m_Buttons.Add(levelbutton);

        ClickedToolbarButton(0);
    }

    void ClickedToolbarButton(int index)
    {
        for(int i=0;i<m_Buttons.Count;i++)
        {
            m_Buttons[i].SetEnabled(index != i);
        }
    }

    void ShowTilesTab()
    {

    }
}