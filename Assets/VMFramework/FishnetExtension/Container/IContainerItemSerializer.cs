#if FISHNET
using FishNet.Serializing;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    [Preserve]
    public static class IContainerItemSerializer
    {
        public static void WriteIContainerItem(this Writer writer, IContainerItem containerItem)
        {
            containerItem.WriteGameItem(writer);
        }
        
        public static IContainerItem ReadIContainerItem(this Reader reader)
        {
            return reader.ReadGameItem<IContainerItem>();
        }
    }
}
#endif