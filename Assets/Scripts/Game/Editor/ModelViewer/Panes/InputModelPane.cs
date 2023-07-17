using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputModelPane : InfoPane
{
    Vector2 scrollPosition;

    public InputModelPane(string tabTitle) : base(tabTitle)
    {
    }

    public override void DrawContents()
    {
        var input = Game.EditorModel.Input;

        using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            using (new EditorGUILayout.VerticalScope())
            {
                GUILayout.Label($"Click Position: {input.ClickPosition}");

                if (input.CurrentInteractable != null)
                {
                    using (new EditorGUILayout.VerticalScope("box"))
                    {
                        EditorGUILayout.LabelField("Current Interactable", EditorStyles.boldLabel);
                        EditorGUILayout.LabelField($"Id: {input.CurrentInteractable.Value}");
                    }
                }

                using (new EditorGUILayout.VerticalScope("box"))
                {
                    EditorGUILayout.LabelField("Interactable Targets", EditorStyles.boldLabel);
                    foreach (var interactable in input.InteractableTargets)
                    {
                        using (new EditorGUILayout.VerticalScope("box"))
                        {
                            EditorGUILayout.LabelField(interactable.Key.ToString(), EditorStyles.boldLabel);
                            ModelDrawers.DrawInteractable(interactable.Value);
                        }
                    }
                }
            }

            scrollPosition = scrollScope.scrollPosition;
        }
    }
}
