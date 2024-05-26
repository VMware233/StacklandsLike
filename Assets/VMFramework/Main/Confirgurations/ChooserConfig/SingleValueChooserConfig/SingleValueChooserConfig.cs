using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class SingleValueChooserConfig<T> : ChooserConfig<T>
    {
        [HideLabel]
        public T value;

        public SingleValueChooserConfig()
        {
            value = default;
        }
        
        public SingleValueChooserConfig(T value)
        {
            this.value = value;
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            if (value is IConfig config)
            {
                config.Init();
            }
            else if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is IConfig itemConfig)
                    {
                        itemConfig.Init();
                    }
                }
            }
        }

        public override IChooser<T> GenerateNewObjectChooser()
        {
            return new SingleValueChooser<T>(value);
        }

        public override IEnumerable<T> GetAvailableValues()
        {
            yield return value;
        }

        public override void SetAvailableValues(Func<T, T> setter)
        {
            value = setter(value);
        }

        public override string ToString()
        {
            if (value is IEnumerable enumerable)
            {
                return enumerable.Cast<object>().Join(", ");
            }
            
            return ValueToString(value);
        }

        public static implicit operator T(SingleValueChooserConfig<T> config)
        {
            return config.value;
        }
        
        public static implicit operator SingleValueChooserConfig<T>(T value)
        {
            return new SingleValueChooserConfig<T>(value);
        }
    }
}