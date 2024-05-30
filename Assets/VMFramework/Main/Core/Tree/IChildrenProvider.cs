using System.Collections.Generic;

namespace VMFramework.Core
{
    public interface IChildrenProvider<out T> where T : class, IChildrenProvider<T>
    {
        public IEnumerable<T> GetChildren();
    }
}