using System;
using VMFramework.Containers;
using VMFramework.GameEvents;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    public partial class ContainerUIManager : IManagerBehaviour
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            ContainerDestroyEvent.AddCallback(OnContainerDestroy);
            
            onDone();
        }

        private static void OnContainerDestroy(ContainerDestroyEvent gameEvent)
        {
            foreach (var panel in openedPanels)
            {
                foreach (var container in panel.GetBindContainers())
                {
                    if (gameEvent.container == container)
                    {
                        panel.Close();
                        
                        break;
                    }
                }
            }
        }
    }
}