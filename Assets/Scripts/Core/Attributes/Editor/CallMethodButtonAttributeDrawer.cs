using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(CallMethodButtonAttribute))]
public class CallMethodButtonAttributeDrawer : PropertyDrawer
{
    CallMethodButtonAttribute button => attribute as CallMethodButtonAttribute;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var rect = position;
        rect.height = 16;

        using (var cc = new EditorGUI.ChangeCheckScope())
        {
            EditorGUI.PropertyField(rect, property, label);
            if(cc.changed)
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        rect.y += 20;
        if (GUI.Button(rect, button.Label))
        {
            Call(property);
        }
    }

    void Call(SerializedProperty property)
    {
        var obj = property.serializedObject;
        if (obj.targetObject is MonoBehaviour)
        {
            Type type = obj.targetObject.GetType();
            MethodInfo info = type.GetMethod(button.MethodName, BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public);
            if (info != null)
            {
                info.Invoke(obj.targetObject, null);
            }
            else
            {
                Debug.LogErrorFormat("Method \"{0}\" not a member of type \"{1}\"", button.MethodName, type.Name);
            }
        }
    }
}
