using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public partial class ProcedureManager
    {
        [ShowInInspector]
        public static bool isLoading { get; private set; } = false;

        [ShowInInspector]
        private static Dictionary<string, Dictionary<ProcedureLoadingType, List<IGameInitializer>>>
            gameInitializers = new();

        [ShowInInspector]
        private static HashSet<IGameInitializer> leftInitializers = new();

        public static void CollectGameInitializers()
        {
            foreach (var derivedClass in typeof(IGameInitializer).GetDerivedClasses(false, false))
            {
                if (derivedClass.TryGetAttribute<GameInitializerRegister>(false,
                        out var register) == false)
                {
                    continue;
                }

                if (register.ProcedureID.IsNullOrEmpty())
                {
                    Debug.LogError(
                        $"{derivedClass}'s {nameof(GameInitializerRegister)} Attribute " +
                        $"has an empty {nameof(GameInitializerRegister.ProcedureID)}");
                    continue;
                }

                if (HasProcedure(register.ProcedureID) == false)
                {
                    Debug.LogError(
                        $"ProcedureManager does not have a Procedure with ID {register.ProcedureID} " +
                        $"required by {derivedClass}'s {nameof(GameInitializerRegister)} Attribute");
                    continue;
                }

                if (derivedClass.CreateInstance() is not IGameInitializer initializer)
                {
                    Debug.LogError(
                        $"Failed to create instance of {derivedClass} as an IGameInitializer");
                    continue;
                }

                if (gameInitializers.TryGetValue(register.ProcedureID, out var initializersByLoadingType) == false)
                {
                    initializersByLoadingType = new();
                    gameInitializers.Add(register.ProcedureID, initializersByLoadingType);
                }

                if (initializersByLoadingType.TryGetValue(register.LoadingType, out var initializers) == false)
                {
                    initializers = new();
                    initializersByLoadingType.Add(register.LoadingType, initializers);
                }
                
                initializers.Add(initializer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyList<IGameInitializer> GetGameInitializers(string procedureID,
            ProcedureLoadingType loadingType)
        {
            if (gameInitializers.TryGetValue(procedureID, out var initializersByLoadingType) == false)
            {
                return Array.Empty<IGameInitializer>();
            }

            if (initializersByLoadingType.TryGetValue(loadingType, out var initializers) == false)
            {
                return Array.Empty<IGameInitializer>();
            }

            return initializers;
        }

        private static async UniTask StartLoading(IReadOnlyList<IGameInitializer> initializers)
        {
            if (isLoading)
            {
                Debug.LogWarning("Loading already in progress.");
                return;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async UniTaskVoid StartLoading(string procedureID, ProcedureLoadingType loadingType,
            Action onFinish = null)
        {
            var initializers = GetGameInitializers(procedureID, loadingType);
            await StartLoading(initializers);
            onFinish?.Invoke();
        }
    }
}