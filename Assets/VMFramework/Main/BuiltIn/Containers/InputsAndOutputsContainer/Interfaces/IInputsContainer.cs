using VMFramework.Core;

namespace VMFramework.Containers
{
    public interface IInputsContainer : IContainer
    {
        public IKCube<int> inputsRange { get; }
    }
}