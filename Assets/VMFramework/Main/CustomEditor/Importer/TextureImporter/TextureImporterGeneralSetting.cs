#if UNITY_EDITOR
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed partial class TextureImporterGeneralSetting : GeneralSetting
    {
        private const string TEXTURE_IMPORTER_CATEGORY = "图片导入设置";
        
        [LabelText("配置"), TabGroup(TAB_GROUP_NAME, TEXTURE_IMPORTER_CATEGORY)]
        [ListDrawerSettings(CustomAddFunction = nameof(AddConfiguration))]
        [JsonProperty]
        public List<TextureImporterConfiguration> configurations = new();

        #region GUI

        private TextureImporterConfiguration AddConfiguration() => new TextureImporterConfiguration();

        #endregion
    }
}
#endif