using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        private class GameTypeInfo : BaseConfig, IChildrenProvider<GameTypeInfo>, IIDOwner, INameOwner,
            ILocalizedNameOwner
        {
            [LabelText("ID")]
            [IsNotNullOrEmpty, IsGameTypeID]
            [JsonProperty]
            public string id;

            [LabelText("Sub Game Types")]
            [JsonProperty]
            public List<GameTypeInfo> subtypes = new();

            [HideInEditorMode]
            public string parentID;

            #region Interface Implementation

            string IIDOwner<string>.id => id;

            string INameOwner.name => id.ToPascalCase(" ");

            public IEnumerable<GameTypeInfo> GetChildren() => subtypes;

            IReadOnlyLocalizedStringReference ILocalizedNameOwner.nameReference => new LocalizedStringReference()
            {
                defaultValue = id.ToPascalCase(" ")
            };

            #endregion
        }
    }
}