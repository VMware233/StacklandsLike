using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class CircularSelectChooserConfig<T> : ChooserConfig<T>, ICircularSelectChooserConfig<T>
    {
        [LabelText("从第几个开始循环"), SuffixLabel("从0开始计数")]
        [MinValue(0)]
        [JsonProperty]
        public int startCircularIndex = 0;

        [LabelText("乒乓循环")]
        [PropertyTooltip("循环到底后，从后往前遍历")]
#if UNITY_EDITOR
        [ShowIf(nameof(showPingPongOption))]
#endif
        [JsonProperty]
        public bool pingPong = false;

        [LabelText("循环体")]
#if UNITY_EDITOR
        [ListDrawerSettings(CustomAddFunction = nameof(AddItemGUI), ShowFoldout = true)]
        [OnValueChanged(nameof(OnItemsChangedGUI), true)]
        [OnCollectionChanged(nameof(OnItemsChangedGUI))]
#endif
        [IsNotNullOrEmpty]
        [JsonProperty]
        public List<CircularSelectItemConfig<T>> items = new();

        protected override void OnInit()
        {
            base.OnInit();
            
            foreach (var item in items)
            {
                if (item.value is IConfig config)
                {
                    config.Init();
                }
                else if (item.value is IEnumerable enumerable)
                {
                    foreach (var obj in enumerable)
                    {
                        if (obj is IConfig configObj)
                        {
                            configObj.Init();
                        }
                    }
                }
            }
        }

        public override IChooser<T> GenerateNewObjectChooser()
        {
            return new CircularSelectChooser<T>(items, pingPong, startCircularIndex);
        }

        public override IEnumerable<T> GetAvailableValues()
        {
            return items.Select(item => item.value);
        }

        public override void SetAvailableValues(Func<T, T> setter)
        {
            foreach (var item in items)
            {
                item.value = setter(item.value);
            }
        }

        public bool ContainsValue(T value)
        {
            return items.Any(item => item.value.Equals(value));
        }

        public void AddValue(T value)
        {
            items.Add(new CircularSelectItemConfig<T> { value = value });
#if UNITY_EDITOR
            OnItemsChangedGUI();
#endif
        }

        public void RemoveValue(T value)
        {
            items.RemoveAll(item => item.value.Equals(value));
#if UNITY_EDITOR
            OnItemsChangedGUI();
#endif
        }

        public override string ToString()
        {
            var content = ", ".Join(items.Select(item =>
            {
                if (item.times > 1)
                {
                    return $"{ValueToString(item.value)}:{item.times}次";
                }

                return ValueToString(item.value);
            }));

            if (pingPong)
            {
                content += " 乒乓循环";
            }

            return content;
        }
    }
}