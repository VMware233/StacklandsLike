using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure
{
    public static class ManagerCreatorContainers
    {
        public const string CONTAINER_NAME = "^Core";
        
        public static Transform managerContainer { get; private set; }
        
        private static readonly Dictionary<string, Transform> _managerTypeContainers = new();

        public static IReadOnlyDictionary<string, Transform> managerTypeContainers =>
            _managerTypeContainers;
        
        public static void Init()
        {
            _managerTypeContainers.Clear();

            var managerContainerGameObject = CONTAINER_NAME.FindOrCreateGameObject();
            
            managerContainerGameObject.AssertIsNotNull(nameof(managerContainerGameObject));
            
            managerContainer = managerContainerGameObject.transform;
            
            managerContainer.SetAsFirstSibling();
            
            foreach (var managerType in Enum.GetValues(typeof(ManagerType)).Cast<ManagerType>())
            {
                GetOrCreateManagerTypeContainer(managerType.ToString());
            }
        }
        
        public static Transform GetOrCreateManagerTypeContainer(string managerTypeName)
        {
            if (_managerTypeContainers.TryGetValue(managerTypeName, out var managerTypeContainer))
            {
                return managerTypeContainer;
            }
            
            var managerTypeContainerGameObject =
                managerTypeName.FindOrCreateGameObject(managerContainer);

            managerTypeContainerGameObject.AssertIsNotNull(nameof(managerTypeContainerGameObject));
                
            _managerTypeContainers.Add(managerTypeName, managerTypeContainerGameObject.transform);
            
            return managerTypeContainerGameObject.transform;
        }

        public static IEnumerable<Transform> GetOtherManagerTypeContainers(string managerTypeName)
        {
            return _managerTypeContainers.Values.Where(t => t.name != managerTypeName);
        }
        
        public static IEnumerable<Transform> GetAllManagerTypeContainers()
        {
            return _managerTypeContainers.Values;
        }
    }
}