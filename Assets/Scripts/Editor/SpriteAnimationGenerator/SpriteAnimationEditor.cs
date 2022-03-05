using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class SpriteAnimationEditor : EditorWindow
{
    const string k_SpriteAnimationAssetsPath = "Assets/Prefabs";

    [MenuItem("Window/Sprite Animation Editor")]
    public static void ShowExample()
    {
        SpriteAnimationEditor wnd = GetWindow<SpriteAnimationEditor>();
        wnd.titleContent = new GUIContent("SpriteAnimationEditor");
    }

    SpriteAnimation m_CurrentAnimation;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/SpriteAnimationGenerator/SpriteAnimationEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // setup elements
        ObjectField rootField = root.Query<ObjectField>("rootObject");
        rootField.objectType = typeof(GameObject);
        rootField.RegisterValueChangedCallback(e => OnChangedRootObject((GameObject)e.newValue));
    }

    void InitCreateNewUI()
    {
        var info = rootVisualElement.Q("infoRoot");
        info.Clear();

        var b = new Button(CreateNewSpriteAnimationAsset);
        b.text = "Create New Asset";

        info.Add(b);
    }

    void InitConfigureSpriteAnimationUI()
    {

    }

    void OnChangedRootObject(GameObject root)
    {
        if (root == null)
        {
            InitCreateNewUI();
        }
        else
        {
            var animation = root.GetComponent<SpriteAnimation>();
            if (animation == null)
            {
                InitConfigureSpriteAnimationUI();
            }
            else
            {
                // update fields
            }
        }
    }

    void CreateNewSpriteAnimationAsset()
    {
        Debug.Log("Created.");
    }



}