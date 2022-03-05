using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;

namespace Editors.Spriteanimation
{
    public class SpriteAnimationEditor : EditorWindow
    {

        [MenuItem("Window/Sprite Animation Editor")]
        public static void ShowExample()
        {
            SpriteAnimationEditor wnd = GetWindow<SpriteAnimationEditor>();
            wnd.titleContent = new GUIContent("Sprite Animation Editor");
            wnd.Focus();
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
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/SpriteAnimationGenerator/SpriteAnimationEditor.uxml");
            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            m_SpriteAnimationTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/SpriteAnimationGenerator/SpriteAnimationInfo.uxml");

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
            var b = new Button(() => {
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
            var b = new Button(() => {
                var go = (GameObject)m_RootObjectField.value;
                m_PrefabController.ConfigureNewSpriteAnimation(go);
                m_RootObjectField.value = go;
            });
            b.text = "Configure New Sprite Animation";
            info.Add(b);
        }

        void InitConfigureSpriteAnimationUI()
        {
            var info = rootVisualElement.Q("infoRoot");
            info.Clear();
            var t = m_SpriteAnimationTree.Instantiate();
            var sprite = t.Q<ObjectField>("sprite");
            sprite.objectType = typeof(Sprite);
            var apply = t.Q<Button>("apply");
            apply.clicked += m_PrefabController.ApplySpriteAnimationChanges;
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
}