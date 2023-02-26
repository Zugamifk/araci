using Narrative;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NarrativeModelListPane : ModelListPane<NarrativeModel>
{
    NarrativeDataCollection narrativeDataCollection;

    public NarrativeModelListPane(string tabTitle, IIdentifiableLookup<NarrativeModel> collection) : base(tabTitle, collection)
    {
        narrativeDataCollection = DataService.GetData<NarrativeDataCollection>();
    }

    protected override void DrawItemData(NarrativeModel item)
    {
        EditorGUILayout.LabelField($"Narrative Key: {item.NarrativeKey}");
        EditorGUILayout.LabelField($"Current State Index: {item.CurrentStateIndex}");

        var data = narrativeDataCollection.GetData(item.NarrativeKey);
        if (item.CurrentStateIndex >= 0 && item.CurrentStateIndex < data.StateCount)
        {
            var state = data.GetStateData(item.CurrentStateIndex);

            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.LabelField($"Name: {state.DisplayName}");

                GUIStyle textStyle = EditorStyles.label;
                textStyle.wordWrap = true;
                EditorGUILayout.LabelField($"Name: {state.Description}", textStyle);
            }
        }
    }
}
