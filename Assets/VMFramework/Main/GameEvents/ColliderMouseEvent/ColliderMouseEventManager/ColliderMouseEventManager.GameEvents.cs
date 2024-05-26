using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public partial class ColliderMouseEventManager
    {
        [ShowInInspector]
        private static readonly Dictionary<MouseEventType, ColliderMouseEvent> mouseEvents = new();

        [Button]
        private static void Invoke(MouseEventType eventType, ColliderMouseEventTrigger trigger)
        {
            if (mouseEvents.TryGetValue(eventType, out ColliderMouseEvent mouseEvent) == false)
            {
                return;
            }
            
            mouseEvent.SetTrigger(trigger);
            
            mouseEvent.Propagate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCallback(MouseEventType eventType, Action<ColliderMouseEvent> callback,
            int priority = GameEventPriority.TINY)
        {
            if (mouseEvents.TryGetValue(eventType, out ColliderMouseEvent gameEvent) == false)
            {
                gameEvent = IGameItem.Create<ColliderMouseEvent>(ColliderMouseEventConfig.ID);
                mouseEvents.Add(eventType, gameEvent);
            }

            gameEvent.AddCallback(callback, priority);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCallback(MouseEventType eventType, Action<ColliderMouseEvent> callback)
        {
            if (mouseEvents.TryGetValue(eventType, out ColliderMouseEvent gameEvent) == false)
            {
                Debug.LogWarning($"{nameof(ColliderMouseEvent)} for {eventType} not found.");
                return;
            }

            gameEvent.RemoveCallback(callback);
        }
    }
}