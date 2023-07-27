using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionModelListPane : ModelListPane<PositionModel>
{
    public PositionModelListPane(string tabTitle, IIdentifiableLookup<PositionModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(PositionModel item)
    {
        ModelDrawers.DrawPosition(item);
    }
}
