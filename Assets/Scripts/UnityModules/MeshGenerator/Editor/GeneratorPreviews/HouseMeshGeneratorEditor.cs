using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MeshGenerator.Wireframes;
using System;

namespace MeshGenerator.Editor
{
    [MeshGeneratorEditor(typeof(HouseMeshGenerator))]
    public class HouseMeshGeneratorEditor : IMeshGeneratorEditor
    {
        HouseMeshGenerator _generator;

        public void DrawInspectorGUI()
        {
            var d = _generator.Data;

            bool rebuildWireframe = false;

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
            d.Rotation = EditorGUILayout.FloatField("Rotation", d.Rotation);
            d.FloorDimensions = EditorGUILayout.Vector2Field("Floor Dimensions", d.FloorDimensions);
            d.BaseExtents = EditorGUILayout.FloatField("Wall Inset", d.BaseExtents);
            d.Height = EditorGUILayout.FloatField("Height", d.Height);
            d.WindowHeight = EditorGUILayout.FloatField("Window Height", d.WindowHeight);
            d.RoofPeak = EditorGUILayout.FloatField("Roof Peak", d.RoofPeak);
            d.EavesLength = EditorGUILayout.FloatField("Eaves Length", d.EavesLength);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Walls", EditorStyles.boldLabel);

            for (int i = 0; i < 4; i++)
            {
                EditorGUILayout.LabelField("Wall "+i, EditorStyles.boldLabel);
                var wall = d.Walls[i];
                var windowCount = wall.Windows.Count;
                EditorGUILayout.LabelField("Windows", EditorStyles.boldLabel);
                int newWindowCount = EditorGUILayout.IntField("Count", windowCount);
                if (windowCount != newWindowCount)
                {
                    if (windowCount > newWindowCount)
                    {
                        wall.Windows.RemoveRange(newWindowCount, windowCount - newWindowCount);
                    } else
                    {
                        for(int j=windowCount;j<newWindowCount;j++)
                        {
                            wall.Windows.Add(new HouseMeshGenerator.GeometryData.WindowData());
                        }
                    }
                    rebuildWireframe = true;
                }
                for (int j=0;j<wall.Windows.Count;j++)
                {
                    EditorGUILayout.LabelField("Window "+j, EditorStyles.boldLabel);
                    var window = wall.Windows[j];
                    window.Dimensions = EditorGUILayout.Vector2Field("Dimensions", window.Dimensions);
                    window.Position = EditorGUILayout.Slider("Position", window.Position, 0, 1);
                }
                EditorGUILayout.Space(5);
            }

            d.Door.Dimensions = EditorGUILayout.Vector2Field("Door Dimensions", d.Door.Dimensions);
            d.Door.Position = EditorGUILayout.Slider("Door Position", d.Door.Position, 0, 1);
            d.Door.Wall = EditorGUILayout.IntSlider("Door Wall", d.Door.Wall, 0, 3);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(d);
            }

            if (rebuildWireframe)
            {
                _generator.BuildWireframe();
            }
        }

        public void DrawSceneGUI(Transform rootTransform)
        {
            Handles.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(_generator.Data.Rotation, Vector3.up), Vector3.one);
            WireframeDrawer.Draw(_generator.Wireframe);
        }

        public void SetGenerator(IGeometryGenerator generator)
        {
            _generator = (HouseMeshGenerator)generator;
            _generator.BuildWireframe();
        }
    }
}
