using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using UnityEditor.Animations;

namespace SpriteAnimation
{
    public class SpriteAnimationEditorWindow : EditorWindow
    {
        const string CONFIG_PATH = "Assets/Scripts/UnityModules/AdvancedSpriteAnimation/Editor/SpriteAnimationEditorConfig.asset";
        const string NEW_ANIMATION_OPTION = "New Animation...";

        [MenuItem("Window/Sprite Animation")]
        static void Open()
        {
            SpriteAnimationEditorWindow window = CreateInstance<SpriteAnimationEditorWindow>();
            window.Show();
        }

        [SerializeField]
        SpriteAnimationEditorWindowConfig config;

        Dictionary<string, SpriteAnimationData> nameToData = new();
        [SerializeField]
        int dataChoiceIndex;
        string[] dataOptions;

        [SerializeField]
        SpriteAnimationData currentData;
        [SerializeField]
        string newDataName;
        [SerializeField]
        Vector2 clipListScrollPosition;

        SpriteAnimationBuilder builder = new();

        [SerializeField]
        SerializedObject currentData_serObj;
        [SerializeField]
        SerializedProperty[] clipData_serProps;

        private void OnEnable()
        {
            if (config == null)
            {
                config = AssetDatabase.LoadAssetAtPath<SpriteAnimationEditorWindowConfig>(CONFIG_PATH);
            }

            if (config == null)
            {
                throw new System.ArgumentException($"No config found at {CONFIG_PATH}");
            }

            nameToData.Clear();
            var allDataAssets = AssetDatabase.FindAssets("t:SpriteAnimationData", new[] { config.SavePath });
            foreach (var guid in allDataAssets)
            {
                var data = AssetDatabase.LoadAssetAtPath<SpriteAnimationData>(AssetDatabase.GUIDToAssetPath(guid));
                nameToData.Add(data.Name, data);
            }

            UpdateDataOptionsList();

            currentData = null;
        }

        private void OnGUI()
        {
            if(currentData_serObj!=null)
            {
                currentData_serObj.Update();
                UpdateClipDateSerializedObjects();
            }

            var newData = EditorGUILayout.Popup("Animation", dataChoiceIndex, dataOptions);
            if (newData != dataChoiceIndex)
            {
                dataChoiceIndex = newData;
                OnChooseNewData();
            }

            GUILayout.Space(10);

            if (currentData == null)
            {
                ShowCreateGui();
            }
            else
            {
                ShowCurrentDataGui();
                currentData_serObj.ApplyModifiedProperties();
            }
        }

        void OnChooseNewData()
        {
            var name = dataOptions[dataChoiceIndex];
            if (name == NEW_ANIMATION_OPTION)
            {
                currentData = null;
                currentData_serObj = null;
                return;
            }

            currentData = nameToData[name];
            currentData_serObj = new SerializedObject(currentData);

            UpdateClipDateSerializedObjects();
        }

        void ShowCreateGui()
        {
            newDataName = EditorGUILayout.TextField("Name", newDataName);
            using (new EditorGUI.DisabledScope(string.IsNullOrEmpty(newDataName)))
            {
                if (GUILayout.Button("Create"))
                {
                    CreateNewData();
                }
            }
        }

        void CreateNewData()
        {
            currentData = builder.CreateNewAnimationData(newDataName, config.SavePath);
            nameToData.Add(newDataName, currentData);
            UpdateDataOptionsList(newDataName);
            newDataName = string.Empty;
            OnChooseNewData();
        }

        void UpdateDataOptionsList(string chooseOption = null)
        {
            dataChoiceIndex = 0;
            dataOptions = new string[nameToData.Count + 1];
            dataOptions[0] = NEW_ANIMATION_OPTION;
            int i = 1;
            var keys = nameToData.Keys.ToList();
            keys.Sort();
            foreach (var name in keys)
            {
                if (name == chooseOption)
                {
                    dataChoiceIndex = i;
                }
                dataOptions[i++] = name;
            }
        }

        void ShowCurrentDataGui()
        {
            DrawActionsBar();

            DrawClipDataArea();
        }

        void DrawActionsBar()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add New Clip"))
                {
                    CreateNewClip();
                }

                if (GUILayout.Button("Rebuild"))
                {
                    RebuildSpriteAnimationData();
                }
            }
        }

        void DrawClipDataArea()
        {
            using (new EditorGUILayout.VerticalScope("box"))
            using (var scroll = new EditorGUILayout.ScrollViewScope(clipListScrollPosition))
            {
                for (int i = 0; i < currentData.Clips.Count; i++)
                {
                    DrawClipData(i);
                    if (currentData.Clips.Count > 1 && GUILayout.Button("Delete"))
                    {
                        DeleteClip(i);
                    }
                }

                clipListScrollPosition = scroll.scrollPosition;
            }
        }

        void CreateNewClip()
        {
            builder.CreateNewClipData(currentData);

            // set first new state as default
            if(currentData.Clips.Count == 1)
            {
                UpdateIsDefaultState(currentData.Clips[0]);
            }
        }

        void UpdateClipDateSerializedObjects()
        {
            clipData_serProps = new SerializedProperty[currentData.Clips.Count];
            for(int i=0;i<currentData.Clips.Count; i++)
            {
                clipData_serProps[i] = currentData_serObj.FindProperty("Clips").GetArrayElementAtIndex(i);
            }
        }

        void DrawClipData(int index)
        {
            var clipData = currentData.Clips[index];
            var clipProp = clipData_serProps[index];
            using (new EditorGUILayout.VerticalScope("box"))
            {
                using(new EditorGUI.DisabledScope(clipData.IsDefaultState))
                {
                    var isDefault = EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("IsDefaultState"));
                    if (isDefault != clipData.IsDefaultState)
                    {
                        UpdateIsDefaultState(clipData);
                    }
                }
                
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("Name"));
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("Duration"));
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("Loop"));
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("StartIndex"));
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("FrameCount"));
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("Source"));
            }
        }

        void DeleteClip(int index)
        {
            builder.RemoveClipData(currentData, index);
            UpdateIsDefaultState(currentData.Clips[0]);
        }

        void RebuildSpriteAnimationData()
        {
            builder.RebuildAnimationData(currentData);
        }

        void UpdateIsDefaultState(SpriteAnimationData.ClipData newDefault)
        {
            builder.SetDefaultState(currentData, newDefault);
        }
    }
}