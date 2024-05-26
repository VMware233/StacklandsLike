namespace VMFramework.Core.Pool
{
    public interface IPool<T>
    {
        public int count { get; }
        
        /// <summary>
        /// Get an object from the pool, if there is no object in the pool, a new object will be created automatically,
        /// and the isFreshlyCreated variable will be returned to indicate whether it is a newly created object.
        /// </summary>
        /// <param name="isFreshlyCreated"></param>
        /// <returns></returns>
        public T Get(out bool isFreshlyCreated);

        /// <summary>
        /// Return an object to the pool.
        /// </summary>
        /// <param name="item"></param>
        public void Return(T item);

        /// <summary>
        /// Clear the pool, all objects in the pool will be destroyed.
        /// </summary>
        public void Clear();
    }
}
