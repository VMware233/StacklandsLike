using System.Collections.Generic;
using VMFramework.Containers;

namespace VMFramework.UI
{
    public interface IContainerUIPanel : IUIPanelController
    {
        public int containerUIPriority { get; }
        
        public IEnumerable<IContainer> GetBindContainers();

        public void SetBindContainer(IContainer newBindContainer);
    }
}