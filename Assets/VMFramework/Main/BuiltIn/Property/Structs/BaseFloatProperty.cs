using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    [PreviewComposite]
    public struct BaseFloatProperty : IFormattable, IComparable<BaseFloatProperty>
    {
        private float _value;

        [ShowInInspector]
        [DelayedProperty]
        public float value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _value;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldHealth = _value;
                _value = value;
                OnValueChanged?.Invoke(oldHealth, _value);
            }
        }

        public event Action<float, float> OnValueChanged;

        public BaseFloatProperty(float value)
        {
            _value = value;
            OnValueChanged = null;
        }

        #region To String

        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }

        public readonly override string ToString()
        {
            return _value.ToString();
        }

        #endregion

        public static implicit operator float(BaseFloatProperty property) =>
            property.value;

        public readonly int CompareTo(BaseFloatProperty other)
        {
            return _value.CompareTo(other._value);
        }
    }
}