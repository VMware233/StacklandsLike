using Sirenix.OdinInspector;
using System;
using System.Runtime.CompilerServices;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    [PreviewComposite]
    public struct BaseBoolProperty
    {
        private bool _value;

        [ShowInInspector]
        [DelayedProperty]
        public bool value
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

        public event Action<bool, bool> OnValueChanged;

        public BaseBoolProperty(bool value)
        {
            _value = value;
            OnValueChanged = null;
        }

        public static implicit operator bool(BaseBoolProperty property) => property.value;
    }
}
