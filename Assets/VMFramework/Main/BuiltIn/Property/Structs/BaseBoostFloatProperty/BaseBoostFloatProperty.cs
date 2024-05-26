using System;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.OdinExtensions;

namespace VMFramework.Property
{
    /// <summary>
    /// 用来表示小数类型带增益的属性，其值 = 基值 * 增益
    /// </summary>
    [PreviewComposite]
    public struct BaseBoostFloatProperty : IFormattable
    {
        /// <summary>
        /// 值 = 基值 * 增益
        /// value = baseValue * boostValue
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        public float value { get; private set; }

        private float _baseValue;

        /// <summary>
        /// 基值
        /// </summary>
        [ShowInInspector]
        public float baseValue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get => _baseValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldBaseValue = _baseValue;
                var oldValue = this.value;
                _baseValue = value;
                this.value = _baseValue * _boostValue;
                this.value = this.value.ClampMin(0);
                OnValueChanged?.Invoke(new(oldBaseValue, boostValue, oldValue),
                    new(baseValue, boostValue, this.value));
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
                this.value = _baseValue * _boostValue;
                this.value = this.value.ClampMin(0);
                OnValueChanged?.Invoke(new(baseValue, oldBoostValue, oldValue),
                    new(baseValue, boostValue, this.value));
            }
        }

        public delegate void OnValueChangedHandler(BaseBoostFloat previous, BaseBoostFloat current);
        
        public event OnValueChangedHandler OnValueChanged;

        public BaseBoostFloatProperty(float baseValue, float boostValue = 1)
        {
            _baseValue = baseValue;
            _boostValue = boostValue;
            value = _baseValue * _boostValue;
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

        public static implicit operator float(BaseBoostFloatProperty property) =>
            property.value;
    }
}
