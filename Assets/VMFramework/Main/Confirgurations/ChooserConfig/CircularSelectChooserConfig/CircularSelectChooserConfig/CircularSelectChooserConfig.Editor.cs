#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Configuration
{
    public partial class CircularSelectChooserConfig<T>
    {
        #region Category

        protected const string CIRCULAR_ACTIONS_CATEGORY = "CircularActions";

        #endregion
        
        private bool showPingPongOption => items is { Count: > 2 };

        #region Add Item GUI

        private CircularSelectItemConfig<T> AddItemGUI()
        {
            CircularSelectItemConfig<T> item = new()
            {
                index = items.Count,
                times = 1,
                value = default,
            };

            return item;
        }

        #endregion

        #region Circular Actions

        [Button("上移")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        private void ShiftUpItems()
        {
            items.Rotate(-1);
            
            OnItemsChangedGUI();
        }

        [Button("下移")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        private void ShiftDownItems()
        {
            items.Rotate(1);
            
            OnItemsChangedGUI();
        }

        [Button("打乱")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        private void ShuffleItems()
        {
            items.Shuffle();
            
            OnItemsChangedGUI();
        }

        [Button("重置循环次数")]
        [ButtonGroup(CIRCULAR_ACTIONS_CATEGORY)]
        private void ResetItemsTimes()
        {
            foreach (var item in items)
            {
                item.times = 1;
            }
        }

        #endregion

        #region On Items Changed GUI

        protected void OnItemsChangedGUI()
        {
            foreach (var (index, item) in items.Enumerate())
            {
                if (item == null)
                {
                    continue;
                }
                
                item.index = index;
            }
        }

        #endregion
    }
}
#endif