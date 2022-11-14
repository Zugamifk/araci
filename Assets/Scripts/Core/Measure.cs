using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// THIS SCRIPT HAS UNIT TESTS!
namespace Core
{
    public struct Measure : IEquatable<Measure>, IComparable, IComparable<Measure>
    {
        float _value;
        public float Value => _value;

        public bool IsEmpty => _value == 0;

        public Measure(float value = 0)
        {
            if (value < 0)
            {
                throw new ArgumentException("Can not create measure with negative _value!");
            }
            _value = value;
        }

        public static implicit operator Measure(float amount) => new Measure(amount);
        public static implicit operator float(Measure measure) => measure.Value;

        public static Measure operator +(Measure a, Measure b) => new Measure(a._value + b.Value);
        public static Measure operator +(Measure a, float b) => new Measure(a._value + b);
        public static Measure operator +(float a, Measure b) => new Measure(a + b.Value);
        public static Measure operator -(Measure a, Measure b) => new Measure(a._value - b.Value);
        public static Measure operator -(Measure a, float b) => new Measure(a._value - b);
        public static Measure operator -(float a, Measure b) => new Measure(a - b.Value);

        public static Measure operator *(Measure a, float b) => new Measure(a.Value * b);
        public static Measure operator *(float a, Measure b) => new Measure(a * b.Value);
        public static Measure operator *(Measure a, Percent b) => new Measure(a.Value * b);
        public static Measure operator *(Percent a, Measure b) => new Measure(a * b.Value);

        public static bool operator <(Measure a, Measure b) => a.Value < b.Value;
        public static bool operator <=(Measure a, Measure b) => a.Value <= b.Value;
        public static bool operator >(Measure a, Measure b) => a.Value > b.Value;
        public static bool operator >=(Measure a, Measure b) => a.Value >= b.Value;
        public static bool operator ==(Measure a, Measure b) => a.Value == b.Value;
        public static bool operator !=(Measure a, Measure b) => a.Value != b.Value;

        public bool Equals(Measure other)
        {
            return other == this;
        }

        public int CompareTo(object obj)
        {
            if (obj == default) return 1;

            var measure = (Measure)obj;
            return _value.CompareTo(measure._value);
        }

        public int CompareTo(Measure other) => Value.CompareTo(other.Value);

        public override bool Equals(object obj)
        {
            if (obj == default) return false;

            if(obj is Measure measure)
            {
                return measure.Value == _value;
            } else if(obj is float f)
            {
                return f == _value;
            } else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return $"Measure: {_value}";
        }
    }
}
