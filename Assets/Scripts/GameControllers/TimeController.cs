using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    float m_GameTime;

    public int Seconds => Mathf.FloorToInt(m_GameTime);

    public void Update(float delta)
    {
        m_GameTime += delta;
    }
}
