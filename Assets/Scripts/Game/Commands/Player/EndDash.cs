using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDash : ICommand
{
    public void Execute(GameModel model)
    {
        model.Player.Dash.IsDashing = false;
    }
}
