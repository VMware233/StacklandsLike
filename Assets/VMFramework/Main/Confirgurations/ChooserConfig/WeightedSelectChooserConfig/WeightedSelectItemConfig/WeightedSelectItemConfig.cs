using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectItemConfig<T> : BaseConfig, IWeightedSelectItem<T>, ICloneable
    {
        [HideLabel]
        [JsonProperty]
        public T value;

        [LabelText("占比"), LabelWidth(30), HorizontalGroup]
        [JsonProperty]
        public int ratio;

        [LabelText("概率"), LabelWidth(30), SuffixLabel("%", Overlay = true), HorizontalGroup]
        [DisplayAsString]
        [NonSerialized]
        public float probability;

        [HideLabel, HorizontalGroup]
        [GUIColor("@Color.yellow")]
        [DisplayAsString]
        [NonSerialized]
        public string tag;

        public object Clone()
        {
            return new WeightedSelectItemConfig<T>()
            {
                value = value,
                ratio = ratio,
            };
        }

        T IWeightedSelectItem<T>.value => value;

        float IWeightedSelectItem<T>.weight => ratio;
    }
}