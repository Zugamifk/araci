using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeModel : ITimeModel
{
    public const float TIME_MULTIPLIER = 100;
    const int SECOND_PER_MINUTE = 60;
    const int MINUTES_PER_HOUR = 60;

    public float LastDeltaTime;
    public TimeSpan RealTime;
    public float Time => (float)RealTime.TotalSeconds * TIME_MULTIPLIER;
    public int Hour => Mathf.FloorToInt(Time) /(SECOND_PER_MINUTE*MINUTES_PER_HOUR);
    public int Minute => Mathf.FloorToInt(Time) / SECOND_PER_MINUTE;

}
