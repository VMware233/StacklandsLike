using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract partial class DefaultContainerItemConsumptionConfig<TItem, TItemPrefab> : BaseConfig, 
        IItemConsumption
        where TItem : IGameItem, IContainerItem
        where TItemPrefab : IGamePrefab
    {
#if UNITY_EDITOR
        [HideLabel]
        [ValueDropdown(nameof(GetItemPrefabNameList))]
        [IsNotNullOrEmpty]
#endif
        public string itemID;

        [MinValue(0)]
        public int count = 1;

        public override string ToString()
        {
            return $"({itemID}, {count})";
        }

        #region Interface

        string IItemConsumption.itemID => itemID;

        int IItemConsumption.count => count;

        #endregion
    }
}