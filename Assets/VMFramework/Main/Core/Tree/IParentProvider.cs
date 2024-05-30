namespace VMFramework.Core
{
    public interface IParentProvider<out T> where T : class, IParentProvider<T>
    {
        public T GetParent();
    }
}