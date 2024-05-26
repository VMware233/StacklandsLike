using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectChooserConfig<T> : ChooserConfig<T>, IWeightedSelectChooserConfig<T>
    {
        [LabelText("按权值随机选择其中的一个")]
#if UNITY_EDITOR
        [OnValueChanged(nameof(OnItemsChangedGUI), true)]
        [OnCollectionChanged(nameof(OnItemsChangedGUI))]
        [ListDrawerSettings(CustomAddFunction = nameof(AddWeightedSelectItemGUI), NumberOfItemsPerPage = 6)]
#endif
        [IsNotNullOrEmpty]
        [JsonProperty]
        public List<WeightedSelectItemConfig<T>> items = new();

        public WeightedSelectChooserConfig()
        {
            
        }

        public WeightedSelectChooserConfig(IEnumerable<T> items)
        {
            this.items = items.Select(item => new WeightedSelectItemConfig<T>
            {
                value = item,
                ratio = 1
            }).ToList();
        }

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
            return new WeightedSelectChooser<T>(items);
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
            items.Add(new WeightedSelectItemConfig<T>
            {
                value = value,
                ratio = 1
            });

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
            if (items.Count == 0)
            {
                return "";
            }

            if (items.Count == 1)
            {
                return $"{ValueToString(items[0].value)}";
            }

            var displayProbabilities = items
                .Select(item => item.ratio).UniqueCount() != 1;

            return ", ".Join(items.Select(item =>
            {
                var itemValueString = ValueToString(item.value);

                if (displayProbabilities)
                {
                    itemValueString +=
                        $":{item.probability.ToString(1)}%";
                }

                return itemValueString;
            }));
        }
    }
}