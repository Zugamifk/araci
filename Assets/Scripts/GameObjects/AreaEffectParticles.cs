using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectParticles : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_Particles;
    [SerializeField]
    float m_BaseArea;
    
    public void UpdateArea(float area)
    {
        area *= m_BaseArea;

        var s = m_Particles.shape;
        switch (s.shapeType)
        {
            case ParticleSystemShapeType.Circle:
                s.radius = area;
                break;
            default:
                break;
        }

        var e = m_Particles.emission;
        e.rateOverTimeMultiplier = area;
    }
}
