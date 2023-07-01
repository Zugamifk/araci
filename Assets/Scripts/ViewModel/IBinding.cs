using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBinding<out T>
{
    event Action<T, T> ValueChanged;
    T Value { get; }
}
