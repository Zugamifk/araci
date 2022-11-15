using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;
using System.Linq;

public class SpriteAnimationEditor : EditorWindow
{

    [MenuItem("Window/Sprite Animation Editor")]
    public static void ShowExample()
    {
        SpriteAnimationEditor wnd = GetWindow<SpriteAnimationEditor>();
        wnd.titleContent = new GUIContent("Sprite Animation Editor");
        wnd.Focus();
        Debug.Log(wnd);
        wnd.Show();
    }

    VisualTreeAsset m_SpriteAnimationTree;

    ObjectField m_RootObjectField;
    PrefabController m_PrefabController;

    public void CreateGUI()
    {
        m_PrefabController = new PrefabController();

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Animation/Editor/SpriteAnimationEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        m_SpriteAnimationTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Animation/Editor/SpriteAnimationInfo.uxml");

        // setup elements
        m_RootObjectField = root.Query<ObjectField>("rootObject");
        m_RootObjectField.objectType = typeof(GameObject);
        m_RootObjectField.RegisterValueChangedCallback(e => OnChangedRootObject((GameObject)e.newValue));

        // init ui
        OnChangedRootObject(null);
    }

    void InitCreateNewUI()
    {
        var info = rootVisualElement.Q("infoRoot");
        info.Clear();
        var b = new Button(() =>
        {
            var go = m_PrefabController.CreateNewSpriteAnimationAsset();
            m_RootObjectField.value = go;
        });
        b.text = "Create New Asset";
        info.Add(b);
    }

    void InitBuildSpriteAnimationUI()
    {
        var info = rootVisualElement.Q("infoRoot");
        info.Clear();
        var b = new Button(() =>
        {
            var go = (GameObject)m_RootObjectField.value;
            m_PrefabController.ConfigureNewSpriteAnimation(go);
        });
        b.text = "Configure New Sprite Animation";
        info.Add(b);
    }

    void InitConfigureSpriteAnimationUI()
    {
        var info = rootVisualElement.Q("infoRoot");
        info.Clear();

        var prefab = (GameObject)m_RootObjectField.value;

        var t = m_SpriteAnimationTree.Instantiate();

        var name = t.Q<TextField>("name");
        name.value = prefab.name;

        var sa = prefab.GetComponent<SpriteAnimation>();
        var texture = t.Q<ObjectField>("sprite");
        texture.objectType = typeof(Texture2D);
        texture.value = sa.Texture;

        var dimensions = t.Q<Vector2IntField>("dimensions");
        var frameCount = t.Q<IntegerField>("frameCount");
        var startIndex = t.Q<IntegerField>("startIndex");
        var time = t.Q<FloatField>("time");
        var loop = t.Q<Toggle>("loop");
        if (sa.AnimationClip != null)
        {
            dimensions.value = sa.Dimensions;
            frameCount.value = sa.FrameCount;
            startIndex.value = sa.StartIndex;
            time.value = sa.AnimationClip.length;
            loop.value = sa.AnimationClip.events.All(e => e.functionName != "Destroy");
        }

        var apply = t.Q<Button>("apply");
        apply.clicked += () => m_PrefabController.ApplySpriteAnimationChanges(
                (GameObject)m_RootObjectField.value,
                name.value,
                (Texture2D)texture.value,
                dimensions.value,
                frameCount.value,
                startIndex.value,
                time.value,
                loop.value
            );
        var delete = t.Q<Button>("delete");
        delete.clicked += () =>
        {
            m_PrefabController.DeleteAssets((GameObject)m_RootObjectField.value);
            m_RootObjectField.value = null;
        };
        info.Add(t);
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
                InitBuildSpriteAnimationUI();
            }
            else
            {
                InitConfigureSpriteAnimationUI();
            }
        }
    }
}
