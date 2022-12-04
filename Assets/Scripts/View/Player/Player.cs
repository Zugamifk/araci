using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    DashEffect _dash;

    bool _wasDashing;

    private void Update()
    {
        var isDashing = Game.Model.Player.Dash.IsDashing;
        if (_wasDashing!=isDashing)
        {
            if(isDashing)
            {
                _dash.DoDash();
            }
            _wasDashing = isDashing;
        }
    }
}
