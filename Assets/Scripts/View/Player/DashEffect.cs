using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    [SerializeField]
    Animator _animation;

    public void DoDash()
    {
        var direction = Game.Model.Characters.GetItem(Game.Model.Player.Id).Movement.Direction;
        direction = Map.Instance.GridToWorldSpace(direction);
        transform.rotation = Math.PointAt(direction);
        _animation.SetTrigger("Dash");
    }
}
