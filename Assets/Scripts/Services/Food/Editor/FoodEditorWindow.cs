using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Food.Editor
{
    public class FoodEditorWindow : EditorWindow
    {
        [MenuItem("Window/Food")]
        static void Open()
        {
            FoodEditorWindow window = ScriptableObject.CreateInstance<FoodEditorWindow>();
            window.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}