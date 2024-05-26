#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Timers
{
    public partial class Timer
    {
        [ShowInInspector]
        public double expectedTimeDebug => _priority;

        [ShowInInspector]
        public double stoppedTimeDebug => stoppedTime;

        [ShowInInspector]
        public double startedTimeDebug => startedTime;
    }
}
#endif