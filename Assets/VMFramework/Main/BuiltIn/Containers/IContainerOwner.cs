using System.Collections.Generic;

namespace VMFramework.Containers
{
    public interface IContainerOwner
    {
        public IEnumerable<IContainer> GetContainers();
    }
}
