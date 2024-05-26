namespace VMFramework.Core.Pool
{
    public interface ILimitedPool<T> : IPool<T>
    {
        public int capacity { get; }
        
        public void Resize(int newCapacity);
    }
}