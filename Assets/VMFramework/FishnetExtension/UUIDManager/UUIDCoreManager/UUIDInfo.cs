#if FISHNET
using System.Collections.Generic;

namespace VMFramework.Network
{
    public struct UUIDInfo
    {
        public IUUIDOwner owner { get; }

        public HashSet<int> observers { get; }

        public bool isObserver { get; set; }

        public UUIDInfo(IUUIDOwner owner, bool asServer)
        {
            this.owner = owner;
            if (asServer)
            {
                observers = new();
            }
            else
            {
                observers = null;
            }
            
            isObserver = false;
        }
    }
}
#endif