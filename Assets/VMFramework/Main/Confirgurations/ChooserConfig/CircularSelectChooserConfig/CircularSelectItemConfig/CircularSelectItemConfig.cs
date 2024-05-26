using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class CircularSelectItemConfig<T> : BaseConfig, ICircularSelectItem<T>, ICloneable
    {
#if UNITY_EDITOR
        [LabelText("@" + nameof(valueLabel)), HorizontalGroup]
#endif
        [JsonProperty]
        public T value;

        [LabelText("循环次数"), HorizontalGroup]
        [MinValue(1)]
        [JsonProperty]
        public int times = 1;

        public object Clone()
        {
            return new CircularSelectItemConfig<T>()
            {
                value = value,
                times = times,
            };
        }

        T ICircularSelectItem<T>.value => value;

        int ICircularSelectItem<T>.times => times;
    }
}