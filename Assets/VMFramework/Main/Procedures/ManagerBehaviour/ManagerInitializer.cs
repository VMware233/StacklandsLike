using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace VMFramework.Procedure
{
    [GameInitializerRegister(typeof(CoreInitializationProcedure))]
    [Preserve]
    public sealed class ManagerInitializer : IGameInitializer
    {
        private static readonly List<IManagerBehaviour> _managerBehaviours = new();

        public static IReadOnlyList<IManagerBehaviour> managerBehaviours =>
            _managerBehaviours;
        
        [ShowInInspector]
        private static readonly HashSet<IManagerBehaviour> leftManagers = new();

        async void IInitializer.OnBeforeInit(Action onDone)
        {
            ManagerCreator.CreateManagers();
            
            var allGameObjects =
                Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (GameObject go in allGameObjects)
            {
                var behaviours = go.GetComponents<IManagerBehaviour>();

                if (behaviours.Length > 0)
                {
                    _managerBehaviours.AddRange(behaviours);
                }
            }
            
            leftManagers.UnionWith(_managerBehaviours);
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnBeforeInit(() => leftManagers.Remove(managerBehaviour));
            }

            await UniTask.WaitUntil(() => leftManagers.Count == 0);
            
            onDone();
        }

        async void IInitializer.OnPreInit(Action onDone)
        {
            leftManagers.UnionWith(_managerBehaviours);
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnPreInit(() => leftManagers.Remove(managerBehaviour));
            }

            await UniTask.WaitUntil(() => leftManagers.Count == 0);
            
            onDone();
        }

        async void IInitializer.OnInit(Action onDone)
        {
            leftManagers.UnionWith(_managerBehaviours);
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnInit(() => leftManagers.Remove(managerBehaviour));
            }

            await UniTask.WaitUntil(() => leftManagers.Count == 0);
            
            onDone();
        }

        async void IInitializer.OnPostInit(Action onDone)
        {
            leftManagers.UnionWith(_managerBehaviours);
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnPostInit(() => leftManagers.Remove(managerBehaviour));
            }

            await UniTask.WaitUntil(() => leftManagers.Count == 0);
            
            onDone();
        }

        async void IInitializer.OnInitComplete(Action onDone)
        {
            leftManagers.UnionWith(_managerBehaviours);
            foreach (var managerBehaviour in _managerBehaviours)
            {
                managerBehaviour.OnInitComplete(() => leftManagers.Remove(managerBehaviour));
            }

            await UniTask.WaitUntil(() => leftManagers.Count == 0);
            
            onDone();
        }
    }
}
