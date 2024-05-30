using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

namespace StackLandsLike.UI
{
    public partial class RecipeCategoryConfig : BaseConfig, IIDOwner
    {
        [GameTypeID]
        public string gameTypeID;
        
        public int priority;

        public LocalizedStringReference categoryName;

        string IIDOwner<string>.id => gameTypeID;
    }
}