using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class TaskUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask DelaySeconds(this float delay)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(delay));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TriggerAction(this Animator target, string triggerName, float triggerTime = 0.05f)
        {
            target.SetBool(triggerName, true);
            DelayAction(triggerTime, () =>
            {
                target.SetBool(triggerName, false);
            });
        }

        #region Delay Action

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void DelayAction(this float delay, Action action)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            action();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void DelayAction(this float delay, Func<UniTaskVoid> func)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));

            func();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelayAction(this int delay, Action action)
        {
            DelayAction(delay.F(), action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelayAction(this int delay, Func<UniTaskVoid> func)
        {
            DelayAction(delay.F(), func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void DelayFrameAction(this int delayFrameCount, Action action)
        {
            await UniTask.DelayFrame(delayFrameCount);

            action();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void DelayAction<T>(this float gap, IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);

                await UniTask.Delay(TimeSpan.FromSeconds(gap));
            }
        }

        #endregion

        #region Repeat

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void Repeat(this int times, float gap, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();

                await UniTask.Delay(TimeSpan.FromSeconds(gap));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void Repeat(this int times, float gap, Func<UniTaskVoid> func)
        {
            for (int i = 0; i < times; i++)
            {
                func();

                await UniTask.Delay(TimeSpan.FromSeconds(gap));
            }
        }

        #endregion
    }
}
