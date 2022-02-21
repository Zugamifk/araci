using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectParticles : MonoBehaviour
{
    [SerializeField]
    ParticleSystem m_Particles;
    [SerializeField]
    float m_BaseArea;
    [SerializeField]
    int m_BaseEmission;
    
    public void UpdateArea(float area)
    {
        var radius = area*m_BaseArea;

        var s = m_Particles.shape;
        switch (s.shapeType)
        {
            case ParticleSystemShapeType.Circle:
                s.radius = radius;
                break;
            default:
                break;
        }

        var e = m_Particles.emission;
        e.rateOverTime = area* area* m_BaseEmission;
    }
}
