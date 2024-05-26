namespace VMFramework.Core
{
    public interface ICircularSelectItem<out T>
    {
        public T value { get; }
        
        public int times { get; }
    }
}