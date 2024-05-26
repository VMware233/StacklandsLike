using VMFramework.Core;

namespace VMFramework.Containers
{
    public interface IOutputsContainer : IContainer
    {
        public IKCube<int> outputsRange { get; }
    }
}