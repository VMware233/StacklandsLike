using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Tasks
{
    public class Task : VisualGameItem, ITask
    {
        [ShowInInspector]
        public bool isDone { get; private set; }
        
        
    }
}