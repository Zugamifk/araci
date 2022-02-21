using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    AreaAttackTrigger m_Area;
    [SerializeField]
    Bullet m_Bullet;

    protected AttackInfo m_Attack;

    private void OnEnable()
    {
        if (m_Area != null)
        {
            m_Area.OnEnemyEnter += Damage;
            m_Area.Enable();
        }
    }

    public void SetAttack(AttackInfo attack)
    {
        m_Attack = attack;
        var area = Services.Find<PlayerController>().CalculateRadius(attack.BaseArea);
        transform.localScale = area * Vector3.one;

        var ps = GetComponent<AreaEffectParticles>();
        if(ps!=null)
        {
            ps.UpdateArea(area);
        }
    }

    public void Enable(Vector3 position, Vector3 direction, AttackInfo attack)
    {
        SetAttack(attack);

        transform.position = position;

        var ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ang - 90);

        if(m_Bullet!=null)
        {
            var s = Services.Find<PlayerController>().CalculateProjectileSpeed(attack.BaseSpeed);
            m_Bullet.Enable(s);
        }

        gameObject.SetActive(true);
    }

    void Damage(IDamageable enemy)
    {
        Services.Find<AttackController>().DoAttack(enemy, m_Attack);
    }
}
