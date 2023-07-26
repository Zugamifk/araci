using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdater : IUpdater
{
    public void Update(GameModel model)
    {
        var playerId = model.Player.Id;
        var playerPos = model.Positions[playerId];
        var camId = model.Camera.Id;
        var camPos = model.Positions[camId];
        camPos.Position.Value = playerPos.Position.Value;
    }
}
