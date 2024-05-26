namespace VMFramework.Core
{
    public interface IWeightedSelectItem<out T>
    {
        public T value { get; }
        public float weight { get; }
    }
}