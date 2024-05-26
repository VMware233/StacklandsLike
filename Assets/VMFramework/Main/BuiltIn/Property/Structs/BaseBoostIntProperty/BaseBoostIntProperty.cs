using System;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    /// <summary>
    /// 用来表示整数类型带增益的属性，其值 = (基值 * 增益)的向下取整
    /// </summary>
    [PreviewComposite]
    public struct BaseBoostIntProperty : IFormattable
    {
        /// <summary>
        /// 值 = (基值 * 增益)的向下取整
        /// value = (baseValue * boostValue).Floor()
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        public int value { get; private set; }

        private int _baseValue;

        /// <summary>
        /// 基值
        /// </summary>
        [ShowInInspector]
        public int baseValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _baseValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldBaseValue = _baseValue;
                var oldValue = this.value;
                _baseValue = value;
                this.value = (_baseValue * _boostValue).Floor();
                this.value = this.value.ClampMin(0);
                OnValueChanged?.Invoke(new(oldBaseValue, _boostValue, oldValue),
                    new(_baseValue, _boostValue, this.value));
            }
        }

        private float _boostValue;

        /// <summary>
        /// 增益
        /// </summary>
        [ShowInInspector]
        public float boostValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _boostValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldBoostValue = _boostValue;
                var oldValue = this.value;
                _boostValue = value;
                this.value = (_baseValue * _boostValue).Floor();
                this.value = this.value.ClampMin(0);
                OnValueChanged?.Invoke(new(baseValue, oldBoostValue, oldValue),
                    new(baseValue, _boostValue, this.value));
            }
        }
        
        public delegate void OnValueChangedHandler(BaseBoostInt previous, BaseBoostInt current);

        public event OnValueChangedHandler OnValueChanged;

        public BaseBoostIntProperty(int baseValue, float boostValue = 1)
        {
            _baseValue = baseValue;
            _boostValue = boostValue;
            value = (_baseValue * _boostValue).Floor();
            value = value.ClampMin(0);
            OnValueChanged = null;
        }

        #region To String

        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            return value.ToString(format, formatProvider);
        }

        public readonly override string ToString()
        {
            return value.ToString();
        }

        #endregion

        public static implicit operator int(BaseBoostIntProperty property) =>
            property.value;
    }
}
