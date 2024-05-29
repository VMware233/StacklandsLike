using Sirenix.OdinInspector;
using StackLandsLike.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed class TaskAndRecipeUIController : UIToolkitPanelController
    {
        private TaskAndRecipeUIPreset taskAndRecipeUIPreset => (TaskAndRecipeUIPreset)preset;

        [ShowInInspector]
        private VisualElement taskContainer;
        
        [ShowInInspector]
        private VisualElement recipeContainer;
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            taskContainer = rootVisualElement.Q(taskAndRecipeUIPreset.taskContainerName);
            recipeContainer = rootVisualElement.Q(taskAndRecipeUIPreset.recipeContainerName);
        }

        private void AddTask(ITask task)
        {
            var toggle = new Toggle
            {
                label = "",
                text = task.name,
                focusable = false
            };
            toggle.RegisterCallback<MouseUpEvent>(e =>
            {
                toggle.value = !toggle.value;
                e.StopPropagation();
            });
            taskContainer.Add(toggle);
        }

        [Button]
        private void AddTask([GamePrefabID] string taskID)
        {
            var task = IGameItem.Create<ITask>(taskID);
            
            AddTask(task);
        }
    }
}