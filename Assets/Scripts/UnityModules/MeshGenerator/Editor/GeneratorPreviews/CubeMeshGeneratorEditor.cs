using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MeshGenerator.Editor
{
    [MeshGeneratorEditor(typeof(CubeMeshGenerator))]
    public class CubeMeshGeneratorEditor : IMeshGeneratorEditor
    {
        public void DrawInspectorGUI()
        {
        }

        public void DrawSceneGUI(Transform rootTransform)
        {
            Handles.color = Color.green;
            Handles.matrix = rootTransform.localToWorldMatrix;
            Handles.DrawWireCube(new Vector3(.5f,.5f,.5f), Vector3.one);
        }

        public void SetGenerator(IGeometryGenerator generator)
        {
        }
    }
}
