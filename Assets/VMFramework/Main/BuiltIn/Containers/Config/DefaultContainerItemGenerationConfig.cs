using Sirenix.OdinInspector;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract partial class DefaultContainerItemGenerationConfig<TItem, TItemPrefab> : BaseConfig
        where TItem : IGameItem, IContainerItem
        where TItemPrefab : IGamePrefab
    {
#if UNITY_EDITOR
        [HideLabel]
        [ValueDropdown(nameof(GetItemPrefabNameList))]
        [IsNotNullOrEmpty]
#endif
        public string itemID;

        [LabelText("数量")]
        [Minimum(0)]
        public IVectorChooserConfig<int> count = new SingleVectorChooserConfig<int>(1);

        public abstract TItem GenerateItem();

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            count.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            count.Init();
        }

        public override string ToString()
        {
            return $"{itemID}, {count}";
        }
    }
}
