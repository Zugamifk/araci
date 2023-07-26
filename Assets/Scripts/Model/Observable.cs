using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable<T> : IObservable<T>
{
    event Action<T, T> valueChanged;

    public event Action<T, T> ValueChanged
    {
        add
        {
            valueChanged += value;
            value?.Invoke(default, Value);
        }
        remove
        {
            valueChanged -= value;
        }
    }

    private T value;
    public T Value
    {
        get => value;
        set
        {
            if (value == null && this.value != null)
            {
                SetValue(value);
            }
            if (!this.value.Equals(value))
            {
                SetValue(value);
            }
        }
    }

    void SetValue(T value)
    {
        T oldValue = this.value;
        this.value = value;
        valueChanged?.Invoke(oldValue, value);
    }

    public Observable() { }
    public Observable(T value)
    {
        Value = value;
    }

    public void SetValueWithoutNotify(T value)
    {
        this.value = value;
    }
}
