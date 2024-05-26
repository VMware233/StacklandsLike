#if FISHNET
using System;
using VMFramework.Network;

namespace VMFramework.Containers
{
    public partial interface IContainer : IUUIDOwner
    {
        public event Action<IContainer> OnOpenOnServerEvent;
        public event Action<IContainer> OnCloseOnServerEvent;
        
        public void OpenOnServer();

        public void CloseOnServer();
    }
}
#endif