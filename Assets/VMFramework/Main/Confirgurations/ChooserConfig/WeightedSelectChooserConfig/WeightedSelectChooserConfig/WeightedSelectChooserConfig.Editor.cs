using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration
{
    public partial class WeightedSelectChooserConfig<T>
    {
        #region Add Item GUI

        private WeightedSelectItemConfig<T> AddWeightedSelectItemGUI()
        {
            return new()
            {
                ratio = 1
            };
        }

        #endregion
        
        #region Contains Same
        
        private bool IsItemsContainsSameValue()
        {
            return items.ContainsSame(item => item.value);
        }
        
        [Button("合并相同的项")]
        [ShowIf(nameof(IsItemsContainsSameValue))]
        private void MergeDuplicates()
        {
            items = items.MergeDuplicates(
                item => item.value,
                (itemA, itemB) =>
                {
                    itemA.ratio += itemB.ratio;
                    return itemA;
                }).ToList();

            OnItemsChangedGUI();
        }

        #endregion

        #region Ratios

        private bool IsRatiosAllZero()
        {
            return items.All(item => item.ratio <= 0);
        }

        private bool IsRatiosCoprime()
        {
            return items.Select(item => item.ratio).AreCoprime();
        }

        [Button("化简占比")]
        [HideIf(nameof(IsRatiosCoprime))]
        private void RatiosToCoprime()
        {
            if (IsRatiosAllZero())
            {
                items.Examine(item => item.ratio = 1);
                return;
            }

            int gcd = items.Select(item => item.ratio).GCD();
            if (gcd > 1)
            {
                items.Examine(item => item.ratio /= gcd);
            }
        }

        #endregion
        
        #region Remove All Null

        protected virtual bool displayRemoveAllNullButton => typeof(T).IsClass;

        [Button("移除所有空值")]
        [ShowIf(nameof(displayRemoveAllNullButton))]
        protected virtual void RemoveAllNull()
        {
            items.RemoveAll(item => item.value is null);
        }

        #endregion

        #region On Items Changed

        protected void OnItemsChangedGUI()
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            items.RemoveAllNull();

            foreach (var item in items)
            {
                item.ratio = item.ratio.Clamp(items.Count == 1 ? 1 : 0,
                    9999);
                item.probability = 0;
                item.tag = "";
            }

            var indicesOfMaxRatio = items
                .GetIndicesOfMaxValues(item => item.ratio).ToList();
            var indicesOfMinRatio = items
                .GetIndicesOfMinValues(item => item.ratio).ToList();

            var totalRatio = items.Sum(item => item.ratio);

            if (totalRatio == 0)
            {
                return;
            }

            foreach (var index in indicesOfMaxRatio.Concat(indicesOfMinRatio))
            {
                var item = items[index];

                bool isMax = indicesOfMaxRatio.Contains(index);
                bool isMin = indicesOfMinRatio.Contains(index);

                if (isMax && isMin)
                {
                    if (indicesOfMaxRatio.Count > 1)
                    {
                        item.tag += "一样";
                    }
                }
                else if (isMax)
                {
                    if (indicesOfMaxRatio.Count > 1)
                    {
                        item.tag += "同时";
                    }

                    item.tag += "最大";
                }
                else if (isMin)
                {
                    if (indicesOfMinRatio.Count > 1)
                    {
                        item.tag += "同时";
                    }

                    item.tag += "最小";
                }
            }

            foreach (var item in items)
            {
                item.probability = 100f * item.ratio / totalRatio;
            }
        }

        #endregion
    }
}