using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public static class InitializerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitializeAll(this IInitializer initializer)
        {
            initializer.OnPreInit(ActionUtility.empty);
            initializer.OnBeforeInit(ActionUtility.empty);
            initializer.OnInit(ActionUtility.empty);
            initializer.OnPostInit(ActionUtility.empty);
            initializer.OnInitComplete(ActionUtility.empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitializeAll(this IEnumerable<IInitializer> initializers)
        {
            var initializersList = new List<IInitializer>(initializers);
            
            foreach (InitializeType initializeType in Enum.GetValues(typeof(InitializeType)))
            {
                initializersList.Initialize(initializeType);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Initialize(this IInitializer initializer, InitializeType type, Action onDone = null)
        {
            onDone ??= ActionUtility.empty;
            
            switch (type)
            {
                case InitializeType.BeforeInit:
                    initializer.OnBeforeInit(onDone);
                    break;
                case InitializeType.PreInit:
                    initializer.OnPreInit(onDone);
                    break;
                case InitializeType.Init:
                    initializer.OnInit(onDone);
                    break;
                case InitializeType.PostInit:
                    initializer.OnPostInit(onDone);
                    break;
                case InitializeType.InitComplete:
                    initializer.OnInitComplete(onDone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Initialize(this IEnumerable<IInitializer> initializers, InitializeType type,
            Action onDone = null)
        {
            foreach (var initializer in initializers)
            {
                initializer.Initialize(type, onDone);
            }
        }
    }
}