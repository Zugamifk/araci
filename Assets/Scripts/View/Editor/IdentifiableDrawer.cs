using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Identifiable))]
public class IdentifiableDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        var id = target as Identifiable;
        using(new EditorGUI.DisabledScope(true))
        {
            GUILayout.Label("Id "+ id.Id.ToString());
        }
    }
}
