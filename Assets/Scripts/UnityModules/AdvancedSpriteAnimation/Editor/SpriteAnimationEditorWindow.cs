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
        const string NEW_CLIP_OPTION = "New Clip...";

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
        string _clipName;
        [SerializeField]
        int _clipChoiceIndex;
        string[] _clipOptions;

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
                Debug.Log(data);
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

            _clipChoiceIndex = 0;
            _clipOptions = new string[_currentData.Clips.Length + 1];
            _clipOptions[0] = NEW_CLIP_OPTION;
            for (int i = 0; i < _currentData.Clips.Length; i++)
            {
                _clipOptions[i + 1] = _currentData.Clips[i].Name;
            }
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

        }
    }
}