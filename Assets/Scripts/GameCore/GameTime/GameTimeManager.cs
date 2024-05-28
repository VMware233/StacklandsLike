using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.GameCore
{
    [ManagerCreationProvider(nameof(GameManagerType.GameCore))]
    public sealed class GameTimeManager : ManagerBehaviour<GameTimeManager>
    {
        /// <summary>
        /// The number of ticks per day.
        /// </summary>
        [ShowInInspector]
        public static int ticksPerDay { get; private set; }
        
        /// <summary>
        /// The current day.
        /// </summary>
        [ShowInInspector]
        public static int day { get; private set; }
        
        /// <summary>
        /// The current tick in the day.
        /// </summary>
        [ShowInInspector]
        public static int tickInDay { get; private set; }

        public static event Action<int> OnDayChanged;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            LogicTickManager.OnPreTick += UpdateTick;
        }

        public static void Init(GameTimeInitInfo info)
        {
            day = info.day;
            tickInDay = info.tickInDay;
            ticksPerDay = info.ticksPerDay;

            if (tickInDay >= ticksPerDay)
            {
                Debug.LogWarning(
                    $"{nameof(tickInDay)} : {tickInDay} is " +
                    $"greater than or equal to {nameof(ticksPerDay)} : {ticksPerDay}.");
                
                var daysToAdd = tickInDay / ticksPerDay;
                day += daysToAdd;
                tickInDay -= daysToAdd * ticksPerDay;
            }
        }

        public static void UpdateTick()
        {
            tickInDay++;

            if (tickInDay >= ticksPerDay)
            {
                day++;
                tickInDay = 0;
                
                OnDayChanged?.Invoke(day);
            }
        }
    }
}