using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Configuration
{
    public partial class DictionaryConfigs<TID, TConfig> : IEnumerable<KeyValuePair<TID, TConfig>>
    {
        public IEnumerator<KeyValuePair<TID, TConfig>> GetEnumerator()
        {
            if (initDone)
            {
                return configsRuntime.GetEnumerator();
            }

            return configs.Select(config => new KeyValuePair<TID, TConfig>(config.id, config))
                .GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}