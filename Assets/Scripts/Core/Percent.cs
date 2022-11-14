using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct Percent
{
    public static readonly Percent Zero = new(0);
    public static readonly Percent OneHundred = new(100);

    readonly float _value;
    readonly bool _allowNegative;
    readonly bool _allowAbove100;

    public float Value => _value;

    public Percent(float value) : this(value, false, false) { }
    public Percent(float value, bool allowNegative, bool allowAbove100)
    {
        if (!allowAbove100 && value > 100)
        {
            value = 100;
        }
        else if (!allowNegative && value < 0)
        {
            value = 0;
        }

        _value = value;
        _allowNegative = allowNegative;
        _allowAbove100 = allowAbove100;
    }

    public static Percent operator -(Percent a) => new Percent(-a._value);
    public static Percent operator +(Percent a, Percent b) => new Percent(a._value + b._value);
    public static Percent operator -(Percent a, Percent b) => new Percent(a._value - b._value);

    public static implicit operator float(Percent a) => a._value;
    public static explicit operator Percent(float a) => new Percent(a);

    public static float operator *(float a, Percent b) => a * b._value / 100;
    public static float operator *(Percent a, float b) => a._value / 100 * b;
}
