using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeModelUpdater : IUpdater
{
    public void Update(GameModel model)
    {
        var timeModel = model.TimeModel;
        timeModel.LastDeltaTime = Time.deltaTime;
        timeModel.RealTime += TimeSpan.FromSeconds(Time.deltaTime);
    }
}
