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
        SpriteAnimationEditorWindowConfig _config;

        Dictionary<string, SpriteAnimationData> _nameToData = new();
        [SerializeField]
        int _dataChoiceIndex;
        string[] _dataOptions;

        [SerializeField]
        SpriteAnimationData _currentData;
        [SerializeField]
        string _newDataName;
        [SerializeField]
        Vector2 _clipListScrollPosition;

        SpriteAnimationBuilder _builder = new();

        [SerializeField]
        SerializedObject _currentData_serObj;
        [SerializeField]
        SerializedProperty[] _clipData_serProps;

        private void OnEnable()
        {
            if (_config == null)
            {
                _config = AssetDatabase.LoadAssetAtPath<SpriteAnimationEditorWindowConfig>(CONFIG_PATH);
            }

            if (_config == null)
            {
                throw new System.ArgumentException($"No config found at {CONFIG_PATH}");
            }

            _nameToData.Clear();
            var allDataAssets = AssetDatabase.FindAssets("t:SpriteAnimationData", new[] { _config.SavePath });
            foreach (var guid in allDataAssets)
            {
                var data = AssetDatabase.LoadAssetAtPath<SpriteAnimationData>(AssetDatabase.GUIDToAssetPath(guid));
                _nameToData.Add(data.Name, data);
            }

            UpdateDataOptionsList();

            _currentData = null;
        }

        private void OnGUI()
        {
            var newData = EditorGUILayout.Popup("Animation", _dataChoiceIndex, _dataOptions);
            if (newData != _dataChoiceIndex)
            {
                _dataChoiceIndex = newData;
                OnChooseNewData();
            }

            GUILayout.Space(10);

            if (_currentData == null)
            {
                ShowCreateGui();
            }
            else
            {
                ShowCurrentDataGui();
            }
        }

        void OnChooseNewData()
        {
            var name = _dataOptions[_dataChoiceIndex];
            if (name == NEW_ANIMATION_OPTION)
            {
                _currentData = null;
                return;
            }

            _currentData = _nameToData[name];
            _currentData_serObj = new SerializedObject(_currentData);

            UpdateClipDateSerializedObjects();
        }

        void ShowCreateGui()
        {
            _newDataName = EditorGUILayout.TextField("Name", _newDataName);
            using (new EditorGUI.DisabledScope(string.IsNullOrEmpty(_newDataName)))
            {
                if (GUILayout.Button("Create"))
                {
                    CreateNewData();
                }
            }
        }

        void CreateNewData()
        {
            _currentData = _builder.CreateNewAnimationData(_newDataName, _config.SavePath);
            _nameToData.Add(_newDataName, _currentData);
            UpdateDataOptionsList(_newDataName);
            _newDataName = string.Empty;
            OnChooseNewData();
        }

        void UpdateDataOptionsList(string chooseOption = null)
        {
            _dataChoiceIndex = 0;
            _dataOptions = new string[_nameToData.Count + 1];
            _dataOptions[0] = NEW_ANIMATION_OPTION;
            int i = 1;
            var keys = _nameToData.Keys.ToList();
            keys.Sort();
            foreach (var name in keys)
            {
                if (name == chooseOption)
                {
                    _dataChoiceIndex = i;
                }
                _dataOptions[i++] = name;
            }
        }

        void ShowCurrentDataGui()
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

            using(new EditorGUILayout.VerticalScope("box"))
            using(var scroll = new EditorGUILayout.ScrollViewScope(_clipListScrollPosition))
            {
                for(int i=0;i<_currentData.Clips.Count;i++)
                {
                    DrawClipData(_currentData.Clips[i], _clipData_serProps[i]);
                }

                _clipListScrollPosition = scroll.scrollPosition;
            }
        }

        void CreateNewClip()
        {
            _builder.CreateNewClipData(_currentData);

            // set first new state as default
            if(_currentData.Clips.Count == 1)
            {
                UpdateIsDefaultState(_currentData.Clips[0]);
            }

            UpdateClipDateSerializedObjects();
        }

        void UpdateClipDateSerializedObjects()
        {
            _clipData_serProps = new SerializedProperty[_currentData.Clips.Count];
            for(int i=0;i<_currentData.Clips.Count; i++)
            {
                _clipData_serProps[i] = _currentData_serObj.FindProperty("Clips").GetArrayElementAtIndex(i);
            }
        }

        void DrawClipData(SpriteAnimationData.ClipData clipData, SerializedProperty clipProp)
        {
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
                EditorGUILayout.PropertyField(clipProp.FindPropertyRelative("AnyStateTransition"));
            }
        }

        void RebuildSpriteAnimationData()
        {
            _builder.RebuildAnimationData(_currentData);
        }

        void UpdateIsDefaultState(SpriteAnimationData.ClipData newDefault)
        {
            _builder.SetDefaultState(_currentData, newDefault);
        }
    }
}