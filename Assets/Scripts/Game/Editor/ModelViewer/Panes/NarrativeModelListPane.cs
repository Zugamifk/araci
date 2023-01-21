using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NarrativeModelListPane : ModelListPane<NarrativeModel>
{
    public NarrativeModelListPane(string tabTitle, IIdentifiableLookup<NarrativeModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(NarrativeModel item)
    {
        EditorGUILayout.LabelField($"Narrative Key: {item.NarrativeKey}");
        EditorGUILayout.LabelField($"Current State Id: {item.CurrentStateId}");
    }
}
