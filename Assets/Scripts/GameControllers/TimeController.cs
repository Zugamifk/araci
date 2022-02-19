using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    float m_GameTime;

    public delegate void ScheduledEvent(int t);

    Dictionary<int, ScheduledEvent> m_TimeToEvent = new Dictionary<int, ScheduledEvent>();

    public int Seconds => Mathf.FloorToInt(m_GameTime);

    public void Update(float delta)
    {
        m_GameTime += delta;
        var n = Mathf.FloorToInt(m_GameTime);
        ScheduledEvent e;
        if(m_TimeToEvent.TryGetValue(n, out e))
        {
            e.Invoke(n);
            m_TimeToEvent.Remove(n);
        }
    }

    public void ScheduleEvent(int time, ScheduledEvent e)
    {
        if(m_TimeToEvent.ContainsKey(time))
        {
            m_TimeToEvent[time] += e;
        } else
        {
            m_TimeToEvent[time] = e;
        }
    }
}
