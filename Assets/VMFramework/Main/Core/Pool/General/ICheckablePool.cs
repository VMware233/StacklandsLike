namespace VMFramework.Core.Pool
{
    public interface ICheckablePool<T> : IPool<T>
    {
        public bool Contains(T item);
    }
}