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
                foreach(var clipData in _currentData.Clips)
                {
                    DrawClipData(clipData);
                }

                _clipListScrollPosition = scroll.scrollPosition;
            }
        }

        void CreateNewClip()
        {
            _builder.CreateNewClipData(_currentData);
        }

        void DrawClipData(SpriteAnimationData.ClipData clipData)
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                clipData.Name = EditorGUILayout.TextField("Name", clipData.Name);
                clipData.Duration = EditorGUILayout.FloatField("Duration", clipData.Duration);
                clipData.Loop = EditorGUILayout.Toggle("Loop Animation", clipData.Loop);
                clipData.StartIndex = EditorGUILayout.IntField("Start Index", clipData.StartIndex);
                clipData.FrameCount = EditorGUILayout.IntField("Frame Count", clipData.FrameCount);
                clipData.Source = (Texture)EditorGUILayout.ObjectField("Source", clipData.Source, typeof(Texture), false);
            }
        }

        void RebuildSpriteAnimationData()
        {
            _builder.RebuildAnimationData(_currentData);
        }
    }
}