using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Procedure
{
    public static class ManagerCreator
    {
        private static readonly HashSet<Type> _abstractManagerTypes = new();

        private static readonly HashSet<Type> _interfaceManagerTypes = new();

        private static readonly HashSet<Type> _managerTypes = new();

        public static IReadOnlyCollection<Type> abstractManagerTypes => _abstractManagerTypes;

        public static IReadOnlyCollection<Type> interfaceManagerTypes => _interfaceManagerTypes;

        public static IReadOnlyCollection<Type> managerTypes => _managerTypes;

        public static void CreateManagers()
        {
            ManagerCreatorContainers.Init();

            var eventCoreContainer =
                ManagerCreatorContainers.GetOrCreateManagerTypeContainer(ManagerType.EventCore.ToString());

            eventCoreContainer.GetOrAddComponent<EventSystem>();
            eventCoreContainer.GetOrAddComponent<StandaloneInputModule>();

            _abstractManagerTypes.Clear();
            _interfaceManagerTypes.Clear();
            _managerTypes.Clear();

            var validManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();
            var invalidManagerClassTypes = new Dictionary<Type, ManagerCreationProviderAttribute>();

            foreach (var managerClassType in typeof(MonoBehaviour).GetDerivedClasses(true, false))
            {
                if (managerClassType.TryGetAttribute<ManagerCreationProviderAttribute>(true,
                        out var providerAttribute) == false)
                {
                    continue;
                }

                if (managerClassType.IsAbstract)
                {
                    _abstractManagerTypes.Add(managerClassType);
                    continue;
                }

                if (managerClassType.IsInterface)
                {
                    _interfaceManagerTypes.Add(managerClassType);
                    continue;
                }

                var isParentOrChild = false;

                foreach (var (validManagerType, validProviderAttribute) in validManagerClassTypes.ToList())
                {
                    if (managerClassType.IsSubclassOf(validManagerType))
                    {
                        validManagerClassTypes.Remove(validManagerType);
                        invalidManagerClassTypes.Add(validManagerType, validProviderAttribute);
                        validManagerClassTypes.Add(managerClassType, providerAttribute);

                        isParentOrChild = true;
                        break;
                    }

                    if (validManagerType.IsSubclassOf(managerClassType))
                    {
                        invalidManagerClassTypes.Add(managerClassType, providerAttribute);

                        isParentOrChild = true;
                        break;
                    }
                }

                if (isParentOrChild == false)
                {
                    validManagerClassTypes.Add(managerClassType, providerAttribute);
                }
            }

            foreach (var (managerClassType, providerAttribute) in invalidManagerClassTypes)
            {
                var managerTypeName = providerAttribute.ManagerTypeName;

                var container = ManagerCreatorContainers.GetOrCreateManagerTypeContainer(managerTypeName);

                container.RemoveFirstComponentImmediate(managerClassType);
            }

            foreach (var (managerClassType, providerAttribute) in validManagerClassTypes)
            {
                var managerTypeName = providerAttribute.ManagerTypeName;

                var container = ManagerCreatorContainers.GetOrCreateManagerTypeContainer(managerTypeName);
                
                container.GetOrAddComponent(managerClassType);
                
                foreach (var otherContainer in ManagerCreatorContainers.GetOtherManagerTypeContainers(managerTypeName))
                {
                    otherContainer.RemoveAllComponentsImmediate(managerClassType);
                }
            }

            _managerTypes.UnionWith(validManagerClassTypes.Keys);

            var managerCreationList = new List<IManagerCreationProvider>();

            if (GameCoreSetting.gameCoreSettingsFile != null)
            {
                if (GameCoreSetting.gameCoreSettingsFile is IManagerCreationProvider managerCreation)
                {
                    managerCreationList.Add(managerCreation);
                }
            }

            foreach (var generalSetting in GameCoreSetting.GetAllGeneralSettings())
            {
                if (generalSetting is IManagerCreationProvider managerCreation)
                {
                    managerCreationList.Add(managerCreation);
                }
            }

            foreach (var managerCreation in managerCreationList)
            {
                managerCreation.HandleManagerCreation();
            }
        }
    }
}