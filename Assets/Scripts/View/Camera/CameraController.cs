using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        var player = Game.Model.Movement.GetItem(Game.Model.Player.Id);
        if(player!=null)
        {
            Map.Instance.PositionObject(player, transform);
        }
    }
}
