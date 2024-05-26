using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.UI;

namespace VMFramework.Property
{
    public abstract class PropertyOfGameItem : GameItem, ITooltipProvider
    {
        protected PropertyConfig propertyConfig => (PropertyConfig)gamePrefab;
        
        private object _target;

        public object target
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _target;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var oldTarget = _target;
                _target = value;
                OnTargetChanged(oldTarget, _target);
            }
        }

        public Sprite icon => propertyConfig.icon;

        public event Action<string> OnValueStringChanged;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetValueString() => propertyConfig.GetValueString(target);

        protected virtual void OnTargetChanged(object previous, object current)
        {

        }

        protected void OnBaseBoostFloatValueChanged(BaseBoostFloat previous, BaseBoostFloat current)
        {
            UpdateValueString();
        }

        protected void OnFloatValueChanged(float previous, float current)
        {
            UpdateValueString();
        }

        protected void OnIntValueChanged(int previous, int current)
        {
            UpdateValueString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void UpdateValueString()
        {
            OnValueStringChanged?.Invoke(GetValueString());
        }

        public string GetTooltipID()
        {
            return GameCoreSetting.propertyGeneralSetting.tooltipID;
        }

        public bool ShowTooltip()
        {
            return propertyConfig.displayTooltip;
        }

        public virtual string GetTooltipTitle()
        {
            return string.Empty;
        }

        public virtual IEnumerable<TooltipPropertyInfo> GetTooltipProperties()
        {
            yield break;
        }

        public virtual string GetTooltipDescription()
        {
            return string.Empty;
        }

        #region Create

        public static PropertyOfGameItem Create(string propertyID, object target)
        {
            var property = IGameItem.Create<PropertyOfGameItem>(propertyID);
            
            property.target = target;
            
            return property;
        }

        #endregion
    }
}