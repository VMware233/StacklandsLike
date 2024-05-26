using VMFramework.Core;

namespace VMFramework.Timers
{
    public interface ITimer : IGenericPriorityQueueNode<double>
    {
        public void OnStart(double startedTime, double expectedTime);
        
        public void OnTimed();

        public void OnStopped(double stoppedTime);
    }
}