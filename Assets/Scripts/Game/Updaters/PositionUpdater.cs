using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : IUpdater
{
    Guid id;

    public PositionUpdater(Guid id)
    {
        this.id = id;
    }

    public void Update(GameModel model)
    {
        var tfService = Services.Get<ITransformService>();
        var tf = tfService.GetTransform(id);
        
        if(tf == null) return;

        var pos = model.Positions[id];
        var gridPos = Services.Get<ITileMapService>().WorldToGridSpace(tf.position);
        pos.Position.SetValueWithoutNotify(gridPos);
    }
}
