using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShrineModelListPane : ModelListPane<ShrineModel>
{
    public ShrineModelListPane(string tabTitle, IIdentifiableLookup<ShrineModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(ShrineModel item)
    {
        EditorGUILayout.LabelField($"Has Blessing Available: {item.HasBlessingAvailable}");
    }
}
