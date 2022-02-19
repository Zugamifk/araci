using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected AttackInfo m_Attack;
    public void Spawn(Vector3 position, Vector3 direction, AttackInfo attack)
    {
        m_Attack = attack;

        transform.position = position;

        var ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang - 90);

        gameObject.SetActive(true);
    }
}
