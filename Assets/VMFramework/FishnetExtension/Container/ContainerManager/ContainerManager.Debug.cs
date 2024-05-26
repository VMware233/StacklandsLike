#if FISHNET && UNITY_EDITOR
using FishNet.Connection;
using FishNet.Object;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Containers
{
    public partial class ContainerManager
    {
        [Button]
        private static void SerializeContainerItemTest(
            [GamePrefabID(true, typeof(IContainerItem))] string containerItemID)
        {
            var containerItem = IGameItem.Create<IContainerItem>(containerItemID);
            containerItem.AssertIsNotNull(nameof(containerItem));
            instance.SerializeContainerItemTestRPC(containerItem);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SerializeContainerItemTestRPC(IContainerItem containerItem,
            NetworkConnection connection = null)
        {
            Debug.LogWarning($"Serializing container item: {containerItem}");
        }
    }
}
#endif