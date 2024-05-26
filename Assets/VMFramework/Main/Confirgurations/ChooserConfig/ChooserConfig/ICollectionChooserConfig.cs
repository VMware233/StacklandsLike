namespace VMFramework.Configuration
{
    public interface ICollectionChooserConfig<T> : IChooserConfig<T>
    {
        public bool ContainsValue(T value);
        
        public void AddValue(T value);
        
        public void RemoveValue(T value);
    }
}