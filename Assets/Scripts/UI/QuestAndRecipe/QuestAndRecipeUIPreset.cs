using System;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed partial class QuestAndRecipeUIPreset : UIToolkitPanelPreset
    {
        public const string ID = "quest_and_recipe_ui";

        public override Type controllerType => typeof(QuestAndRecipeUIController);

        [VisualElementName]
        [IsNotNullOrEmpty]
        public string questContainerName;
        
        [VisualElementName]
        [IsNotNullOrEmpty]
        public string recipeContainerName;

        [GameTypeID]
        public string recipeGameTypeID;
    }
}