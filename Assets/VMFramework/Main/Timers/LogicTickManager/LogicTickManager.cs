using System;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.Timers
{
    [DisallowMultipleComponent]
    [ManagerCreationProvider(ManagerType.TimerCore)]
    public sealed partial class LogicTickManager : ManagerBehaviour<LogicTickManager>
    {
        private static ulong _tick = 0;
        private static float _tickGap = 1 / 60f;
        private static float _timeLeftOver = 0;
        private static bool _enabled = false;
        
        public static ulong tick => _tick;
        
        public static float tickGap => _tickGap;
        
        public static float timeLeftOver => _timeLeftOver;
        
        public static event Action OnPreTick;
        public static event Action OnTick;
        public static event Action OnPostTick;

        public static void IncreaseTick()
        {
            OnPreTick?.Invoke();
            
            OnTick?.Invoke();
            
            OnPostTick?.Invoke();
            
            _tick++;
        }

        private void Update()
        {
            if (_enabled == false)
            {
                return;
            }
            
            _timeLeftOver += Time.deltaTime;

            while (_timeLeftOver >= _tickGap)
            {
                IncreaseTick();
                _timeLeftOver -= _tickGap;
            }
        }
        
        public static void SetTickGap(float tickGap)
        {
            _tickGap = tickGap;
        }

        public static void StartTick()
        {
            _enabled = true;
        }
        
        public static void StopTick()
        {
            _enabled = false;
        }
        
        public static bool IsTicking() => _enabled;
    }
}