#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public class FindGamePrefabWrapperUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "获取GamePrefabWrapper";

        public override bool IsValid(IList<object> selectedObjects)
        {
            foreach (var obj in selectedObjects)
            {
                if (obj is IGamePrefab)
                {
                    return true;
                }

                if (obj is Type type)
                {
                    if (type.IsDerivedFrom<IGamePrefab>(true))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var obj in selectedObjects)
            {
                if (obj is IGamePrefab gamePrefab)
                {
                    foreach (var wrapper in GamePrefabWrapperQuery.GetGamePrefabWrappers(gamePrefab))
                    {
                        yield return wrapper;
                    }
                    
                    continue;
                }

                if (obj is Type type)
                {
                    if (type.IsDerivedFrom<IGamePrefab>(true))
                    {
                        foreach (var wrapper in GamePrefabWrapperQuery.GetGamePrefabWrappers(type))
                        {
                            yield return wrapper;
                        }
                    }
                    continue;
                }
                
                yield return obj;
            }
        }
    }
}
#endif