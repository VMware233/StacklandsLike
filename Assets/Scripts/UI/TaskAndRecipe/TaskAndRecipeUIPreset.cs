using System;
using UnityEngine.UIElements;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class TaskAndRecipeUIPreset : UIToolkitPanelPreset
    {
        public const string ID = "task_and_recipe_ui";

        public override Type controllerType => typeof(TaskAndRecipeUIController);

        [VisualElementName]
        [IsNotNullOrEmpty]
        public string taskContainerName;
        
        [VisualElementName]
        [IsNotNullOrEmpty]
        public string recipeContainerName;
    }
}