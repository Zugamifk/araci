using Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIModelListPane : ModelListPane<AIModel>
{
    public AIModelListPane(string tabTitle, IIdentifiableLookup<AIModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(AIModel item)
    {
        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("Agent", EditorStyles.boldLabel);
            ModelDrawers.DrawAgent(item.Agent);
        }

        using (new EditorGUILayout.VerticalScope("box"))
        {
            EditorGUILayout.LabelField("State", EditorStyles.boldLabel);
            ModelDrawers.DrawBehaviourState(item.State);
        }
    }
}
