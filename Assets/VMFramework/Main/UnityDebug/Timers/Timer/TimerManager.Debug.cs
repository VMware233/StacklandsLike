#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Timers
{
    public partial class TimerManager
    {
        [ShowInInspector]
        private static double currentTimeDebug => currentTime;
        
        [ShowInInspector]
        private static List<ITimer> allTimers => queue.ToList();

        [Button]
        private static void AddTimerDebug(float delay = 5)
        {
            var timer = new Timer(_ => Debug.LogError(233));
            
            Add(timer, delay);
        }
        
        [Button]
        private static void AddGameItemDebug([GamePrefabID(true, typeof(ITimer))] string id, float delay = 5)
        {
            var gameItem = IGameItem.Create(id);

            if (gameItem is ITimer timer)
            {
                Add(timer, delay);
            }
        }
    }
}
#endif