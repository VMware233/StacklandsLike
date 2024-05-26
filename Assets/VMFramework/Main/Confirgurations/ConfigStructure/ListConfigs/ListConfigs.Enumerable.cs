using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Configuration
{
    public partial class ListConfigs<TConfig> : IEnumerable<TConfig>
    {
        public IEnumerator<TConfig> GetEnumerator()
        {
            return configs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}