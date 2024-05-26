namespace VMFramework.Core.Pool
{
    public interface ICheckableLimitPool<T> : ICheckablePool<T>, ILimitedPool<T>
    {

    }
}