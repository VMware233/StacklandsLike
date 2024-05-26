using System;
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

namespace VMFramework.Timers
{
    [ManagerCreationProvider(ManagerType.TimerCore)]

    public class UpdateDelegateManager : UniqueMonoBehaviour<UpdateDelegateManager>
    {
        [ShowInInspector]
        public static int fixedUpdateEventCount = 0;

        [ShowInInspector]
        public static int updateEventCount = 0;

        [ShowInInspector]
        public static int lateUpdateEventCount = 0;

        [ShowInInspector]
        public static int onGUIEventCount = 0;

        public static event Action FixedUpdateEvent;
        public static event Action UpdateEvent;
        public static event Action LateUpdateEvent;
        public static event Action OnGUIEvent;

        [ShowInInspector]
        private static HashSet<Action> allFixedUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> allUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> allLateUpdateDelegates = new();

        [ShowInInspector]
        private static HashSet<Action> allOnGUIDelegates = new();

        public static bool HasUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    return allFixedUpdateDelegates.Contains(action);
                case UpdateType.Update:
                    return allUpdateDelegates.Contains(action);
                case UpdateType.LateUpdate:
                    return allLateUpdateDelegates.Contains(action);
                case UpdateType.OnGUI:
                    return allOnGUIDelegates.Contains(action);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void AddUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    FixedUpdateEvent += action;
                    allFixedUpdateDelegates.Add(action);
                    fixedUpdateEventCount++;
                    break;
                case UpdateType.Update:
                    UpdateEvent += action;
                    allUpdateDelegates.Add(action);
                    updateEventCount++;
                    break;
                case UpdateType.LateUpdate:
                    LateUpdateEvent += action;
                    allLateUpdateDelegates.Add(action);
                    lateUpdateEventCount++;
                    break;
                case UpdateType.OnGUI:
                    OnGUIEvent += action;
                    allOnGUIDelegates.Add(action);
                    onGUIEventCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void RemoveUpdateDelegate(UpdateType type, Action action)
        {
            switch (type)
            {
                case UpdateType.FixedUpdate:
                    FixedUpdateEvent -= action;
                    allFixedUpdateDelegates.Remove(action);
                    fixedUpdateEventCount++;
                    break;
                case UpdateType.Update:
                    UpdateEvent -= action;
                    allUpdateDelegates.Remove(action);
                    updateEventCount++;
                    break;
                case UpdateType.LateUpdate:
                    LateUpdateEvent -= action;
                    allLateUpdateDelegates.Remove(action);
                    lateUpdateEventCount++;
                    break;
                case UpdateType.OnGUI:
                    OnGUIEvent -= action;
                    allOnGUIDelegates.Remove(action);
                    onGUIEventCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }

        private void OnGUI()
        {
            OnGUIEvent?.Invoke();
        }
    }
}
