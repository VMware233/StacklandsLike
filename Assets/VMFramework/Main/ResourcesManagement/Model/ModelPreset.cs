using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;
using Object = UnityEngine.Object;

namespace VMFramework.ResourcesManagement
{
    public class ModelPreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "model";

        [LabelText("现成的模型预制体")]
        [Required]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public GameObject readyMadeModelPrefab;

        public GameObject GetPrefab()
        {
            return readyMadeModelPrefab;
        }
    }
}
