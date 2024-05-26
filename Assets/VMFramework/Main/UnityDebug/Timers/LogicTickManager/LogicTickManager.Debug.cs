#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Timers
{
    public partial class LogicTickManager
    {
        [ShowInInspector]
        private static ulong tickDebug => _tick;
        
        [ShowInInspector]
        private static float tickGapDebug => _tickGap;
        
        [ShowInInspector]
        private static float timeLeftOverDebug => _timeLeftOver;
        
        [ShowInInspector]
        private static bool enabledDebug => _enabled;
        
        [Button]
        private static void SetTickGapDebug(float tickGap)
        {
            SetTickGap(tickGap);
        }

        [Button]
        public static void StartTickDebug()
        {
            StartTick();
        }
        
        [Button]
        public static void StopTickDebug()
        {
            StopTick();
        }
    }
}
#endif