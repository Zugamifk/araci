using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModelListPane : ModelListPane<MovementModel>
{
    public MovementModelListPane(string tabTitle, IIdentifiableLookup<MovementModel> collection) : base(tabTitle, collection)
    {
    }

    protected override void DrawItemData(MovementModel item)
    {
        ModelDrawers.DrawMovement(item);
    }
}
