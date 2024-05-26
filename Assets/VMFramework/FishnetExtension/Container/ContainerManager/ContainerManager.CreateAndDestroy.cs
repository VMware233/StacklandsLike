#if FISHNET
using System;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.Network;
using VMFramework.Procedure;

namespace VMFramework.Containers
{
    public partial class ContainerManager : IManagerBehaviour
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            ContainerCreateEvent.AddCallback(OnContainerCreate, GameEventPriority.SUPER);
            ContainerDestroyEvent.AddCallback(OnContainerDestroy, GameEventPriority.SUPER);
            
            onDone();
        }

        private static void OnContainerCreate(ContainerCreateEvent gameEvent)
        {
            if (gameEvent.container == null)
            {
                Debug.LogError("Container is null in ContainerCreateEvent");
                return;
            }
            
            gameEvent.container.OnOpenEvent += OnContainerOpen;
            gameEvent.container.OnCloseEvent += OnContainerClose;
        }

        private static void OnContainerDestroy(ContainerDestroyEvent gameEvent)
        {
            if (gameEvent.container == null)
            {
                Debug.LogError("Container is null in ContainerDestroyEvent");
                return;
            }
            
            gameEvent.container.OnOpenEvent -= OnContainerOpen;
            gameEvent.container.OnCloseEvent -= OnContainerClose;
            
            UUIDCoreManager.Unregister(gameEvent.container);
        }
        
        private static void OnContainerOpen(IContainer container)
        {
            if (instance.IsServerStarted)
            {
                container.isDirty = false;
            }

            Observe(container);
        }

        private static void OnContainerClose(IContainer container)
        {
            Unobserve(container);
        }
    }
}
#endif