using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        var player = ViewLookup.Get(Game.Model.Player.Id);
        if(player!=null)
        {
            transform.position = player.transform.position;
        }
    }
}
