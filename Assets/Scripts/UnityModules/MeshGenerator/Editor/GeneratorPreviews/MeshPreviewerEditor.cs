using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

namespace MeshGenerator.Editor
{
    [CustomEditor(typeof(MeshPreviewer))]
    public class MeshPreviewerEditor : UnityEditor.Editor
    {
        static Dictionary<Type, IMeshGeneratorEditor> _generatorToPreview;
        static Dictionary<string, IGeometryGenerator> _keyToGenerator;

        int _selectedGeneratorOption;
        string[] _generatorOptions;
        IMeshGeneratorEditor _currentEditor;
        IGeometryGenerator _currentGenerator;
        bool _updateMeshOnChange = false;
        Transform _rootTransform;

        void GetPreviews()
        {
            _generatorToPreview = new();
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes());
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<MeshGeneratorEditorAttribute>();
                if (attr != null)
                {
                    var previewer = (IMeshGeneratorEditor)Activator.CreateInstance(type);
                    _generatorToPreview.Add(attr.GeneratorType, previewer);
                }
            }

            _keyToGenerator = new();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<MeshGeneratorAttribute>();
                if (attr != null)
                {
                    var generator = (IGeometryGenerator)Activator.CreateInstance(type);
                    _keyToGenerator.Add(attr.Key, generator);
                }
            }
            _generatorOptions = _keyToGenerator.Keys.ToArray();
        }

        private void OnEnable()
        {
            GetPreviews();
            
            var transformProp = serializedObject.FindProperty("_generatorTransform");
            _rootTransform = (Transform)transformProp.objectReferenceValue;

            var typeProp = serializedObject.FindProperty("_meshType");
            UpdateEditor(typeProp.stringValue);
        }

        public override void OnInspectorGUI()
        {
            if(_generatorToPreview==null)
            {
                GetPreviews();
            }

            var previewer = target as MeshPreviewer;

            var typeProp = serializedObject.FindProperty("_meshType");
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                var selected = EditorGUILayout.Popup("Mesh Type", Array.IndexOf(_generatorOptions, typeProp.stringValue), _generatorOptions);
                if(_selectedGeneratorOption!=selected)
                {
                    _selectedGeneratorOption = selected;
                    if (_selectedGeneratorOption >= 0 &&_selectedGeneratorOption < _generatorOptions.Length)
                    {
                        typeProp.stringValue = _generatorOptions[_selectedGeneratorOption];
                    }
                }
                if (changeCheck.changed)
                {
                    UpdateEditor(typeProp.stringValue);
                }
            }

            var transformProp = serializedObject.FindProperty("_generatorTransform");
            using (var changeCheck = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(transformProp);
                if (changeCheck.changed)
                {
                    _rootTransform = (Transform)transformProp.objectReferenceValue;
                }
            }

            if (_currentEditor != null)
            {
                using (var cc = new EditorGUI.ChangeCheckScope())
                {
                    _currentEditor.DrawInspectorGUI();

                    if (cc.changed)
                    {
                        if(_updateMeshOnChange)
                        {
                            UpdateMesh(previewer);
                        }
                        SceneView.RepaintAll();
                    }
                }
            }

            _updateMeshOnChange = EditorGUILayout.Toggle("Update Mesh", _updateMeshOnChange);

            if (GUILayout.Button("Generate Mesh"))
            {
                UpdateMesh(previewer);
            }

            if(GUILayout.Button("Clear"))
            {
                previewer.Clear();
            }

            serializedObject.ApplyModifiedProperties();
        }

        void UpdateMesh(MeshPreviewer previewer)
        {
            var result = Generate();
            previewer.SetMesh(result);
        }

        void UpdateEditor(string type)
        {
            _keyToGenerator.TryGetValue(type, out _currentGenerator);

            if (_currentGenerator != null)
            {
                _generatorToPreview.TryGetValue(_currentGenerator.GetType(), out _currentEditor);
                if (_currentEditor != null)
                {
                    _currentEditor.SetGenerator(_currentGenerator);
                }
            }
        }

        public MeshGeneratorResult Generate()
        {
            return _currentGenerator.Generate();
        }

        private void OnSceneGUI()
        {
            if(_currentEditor!=null)
            {
                _currentEditor.DrawSceneGUI(_rootTransform);
            }
        }
    }
}
