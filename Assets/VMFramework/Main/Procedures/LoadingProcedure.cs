using System;
using VMFramework.Core;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Procedure
{
    public abstract class LoadingProcedure : ProcedureBase
    { 
        public abstract string nextProcedureID { get; }
        
        private readonly HashSet<IGameInitializer> leftInitializers = new();
        
        [ShowInInspector]
        private List<Type> leftInitializersTypes => leftInitializers.Select(i => i.GetType()).ToList();
        
        public sealed override void OnEnter()
        {
            base.OnEnter();

            _ = OnEnterLoading();
        }

        protected virtual async UniTask OnEnterLoading()
        {
            List<IGameInitializer> initializers = new();

            foreach (var derivedClass in typeof(IGameInitializer).GetDerivedClasses(
                         false, false))
            {
                if (derivedClass.TryGetAttribute<GameInitializerRegister>(false,
                        out var register) == false)
                {
                    continue;
                }

                if (register.ProcedureType != GetType())
                {
                    continue;
                }

                if (derivedClass.CreateInstance() is IGameInitializer initializer)
                {
                    initializers.Add(initializer);
                }
            }

            foreach (var initializer in initializers)
            {
                Debug.Log($"初始化器 {initializer} 开始初始化");
            }

            foreach (InitializeType initializeType in Enum.GetValues(typeof(InitializeType)))
            {
                leftInitializers.UnionWith(initializers);

                foreach (var initializer in initializers)
                {
                    initializer.Initialize(initializeType, () => leftInitializers.Remove(initializer));
                }
                
                await UniTask.WaitUntil(() => leftInitializers.Count == 0);
            }
        }

        protected void EnterNextProcedure()
        {
            ProcedureManager.AddToSwitchQueue(id, nextProcedureID);
        }
    }
}
