using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Tasks
{
    public interface ITask : IVisualGameItem
    {
        public bool isDone { get; }
    }
}