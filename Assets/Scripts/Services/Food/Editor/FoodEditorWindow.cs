using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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
            if(GUILayout.Button("Roasted Chicken"))
            {
                var recipe = new RoastedChicken();
                var food = recipe.Prepare();
                Debug.Log(food.Description);
            }
        }
    }
}