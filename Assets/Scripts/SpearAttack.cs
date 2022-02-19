using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    public void Spawn(Vector3 position, Vector3 direction, AttackInfo attack)
    {
        transform.position = position;
        var ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang - 180);
    }
}
