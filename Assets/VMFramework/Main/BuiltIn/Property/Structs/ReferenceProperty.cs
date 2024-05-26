using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    [PreviewComposite]
    public struct ReferenceProperty<T> where T : class
    {
        private T _value;

        [ShowInInspector]
        [DelayedProperty]
        public T value
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

        public event Action<T, T> OnValueChanged;

        public ReferenceProperty(T value)
        {
            _value = value;
            OnValueChanged = null;
        }

        public static implicit operator T(ReferenceProperty<T> property) => property.value;
    }
}