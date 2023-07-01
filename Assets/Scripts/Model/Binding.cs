using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binding<T> : IBinding<T>
{
    public event Action<T, T> ValueChanged;

    private T value;
    public T Value
    {
        get => value;
        set
        {
            if (!this.value.Equals(value))
            {
                T oldValue = this.value;
                this.value = value;
                ValueChanged?.Invoke(oldValue, value);
            }
        }
    }

    public Binding() { }
    public Binding(T value)
    {
        Value = value;
    }
}
