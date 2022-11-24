using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeModel
{
    float Time { get; }
    int Hour { get; }
    int Minute { get; }
}
