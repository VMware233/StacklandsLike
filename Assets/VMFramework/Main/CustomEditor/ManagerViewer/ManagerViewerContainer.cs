#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.Editor
{
    internal sealed class ManagerViewerContainer : SimpleOdinEditorWindowContainer
    {
        [ShowInInspector]
        private Transform managerContainer => ManagerCreatorContainers.managerContainer;
        
        [ShowInInspector]
        private IReadOnlyDictionary<string, Transform> managerTypeContainers =>
            ManagerCreatorContainers.managerTypeContainers;
        
        [ShowInInspector]
        private List<Type> abstractManagerTypes;
        
        [ShowInInspector]
        private List<Type> interfaceManagerTypes;
        
        [Searchable]
        [ShowInInspector]
        private List<Type> managerTypes;
        
        [Button]
        private void CreateManagers()
        {
            ManagerCreator.CreateManagers();
        }

        public override void Init()
        {
            base.Init();

            abstractManagerTypes = ManagerCreator.abstractManagerTypes.ToList();
            interfaceManagerTypes = ManagerCreator.interfaceManagerTypes.ToList();
            managerTypes = ManagerCreator.managerTypes.ToList();
        }
    }
}
#endif