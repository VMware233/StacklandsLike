using System.Collections;
using System.Collections.Generic;

namespace VMFramework.Map
{
    public sealed partial class MapCore<TChunk, TTile>
    {
        public abstract partial class Map : IEnumerable<TChunk>
        {
            public IEnumerator<TChunk> GetEnumerator()
            {
                return chunkDictXYZ.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}