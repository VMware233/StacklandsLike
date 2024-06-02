using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    [ManagerCreationProvider(ManagerType.ProcedureCore)]
    public sealed partial class ProcedureManager : UniqueMonoBehaviour<ProcedureManager>
    {
        [ShowInInspector]
        private static IMultiFSM<string, ProcedureManager> fsm = new MultiFSM<string, ProcedureManager>();

        [ShowInInspector]
        private static List<IManagerBehaviour> managerBehaviours = new();

        private static Dictionary<string, IProcedure> _procedures = new();
        
        [ShowInInspector]
        public static IReadOnlyList<IProcedure> procedures => _procedures.Values.ToList();

        [ShowInInspector]
        [ListDrawerSettings(ShowFoldout = false)]
        public static IReadOnlyList<string> currentProcedureIDs => fsm?.currentStatesID.ToList();

        [ShowInInspector]
        private static readonly Queue<(string fromProcedureID, string toProcedureID)> procedureSwitchQueue =
            new();

        public static event Action<string> OnEnterProcedureEvent;
        
        public static event Action<string> OnExitProcedureEvent;

        #region Awake & Start & Update

        private void Start()
        {
            string startProcedureID = null;
            int startProcedurePriority = 0;
            
            foreach (var procedureType in typeof(IProcedure).GetDerivedClasses(false, false))
            {
                if (procedureType.IsAbstract || procedureType.IsInterface)
                {
                    continue;
                }

                var procedure = (IProcedure)procedureType.CreateInstance();

                _procedures.Add(procedure.id, procedure);

                fsm.AddState(procedure);

                if (procedureType.TryGetAttribute<StartProcedureAttribute>(false,
                        out var startProcedureAttribute))
                {
                    if (startProcedureID == null)
                    {
                        startProcedureID = procedure.id;
                        startProcedurePriority = startProcedureAttribute.Priority;
                    }
                    else if (startProcedureAttribute.Priority > startProcedurePriority)
                    {
                        startProcedureID = procedure.id;
                        startProcedurePriority = startProcedureAttribute.Priority;
                    }
                }
            }

            if (startProcedureID == null)
            {
                throw new InvalidOperationException("No start procedure found.");
            }

            fsm.Init(this);

            OnEnterProcedureEvent += procedureID => Debug.Log($"Enter Procedure:<color=orange>{procedureID}</color>");

            OnExitProcedureEvent += procedureID => Debug.Log($"Exit Procedure:<color=orange>{procedureID}</color>");

            ProcedureAutoSwitchBinder.Init(_procedures.Values);

            OnEnterProcedureEvent += procedureID =>
            {
                if (ProcedureAutoSwitchBinder.TryGetNextProcedureID(procedureID, out var nextProcedureID))
                {
                    EnterProcedure(procedureID, nextProcedureID);
                }
            };

            CollectGameInitializers();

            EnterProcedure(startProcedureID);
        }

        private void Update()
        {
            if (procedureSwitchQueue.Count == 0)
            {
                return;
            }

            var (fromProcedureID, toProcedureID) = procedureSwitchQueue.Dequeue();

            if (fromProcedureID.IsNullOrEmpty() == false)
            {
                if (HasCurrentProcedure(fromProcedureID) == false)
                {
                    Debug.LogError($"Failed to switch procedure from {fromProcedureID} to {toProcedureID}. " +
                                   $"{fromProcedureID} is not current state.");
                    EnterProcedureImmediately(toProcedureID);
                }
                else
                {
                    ExitProcedureImmediately(fromProcedureID, () => EnterProcedureImmediately(toProcedureID));
                }
            }
            else
            {
                EnterProcedureImmediately(toProcedureID);
            }
        }

        #endregion

        #region Enter & Exit Procedure

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedureImmediately(string procedureID)
        {
            if (isLoading)
            {
                Debug.LogWarning("ProcedureManager is still loading, cannot switch procedure.");
                return;
            }
            
            if (fsm.HasCurrentState(procedureID))
            {
                Debug.LogWarning($"Procedure with ID:{procedureID} is already current state.");
                return;
            }

            if (fsm.CanEnterState(procedureID) == false)
            {
                Debug.LogError($"Failed to enter procedure with ID:{procedureID}.");
                return;
            }
            
            StartLoading(procedureID, ProcedureLoadingType.OnEnter, () =>
            {
                if (fsm.CanEnterState(procedureID) == false)
                {
                    throw new InvalidOperationException($"Failed to enter procedure with ID:{procedureID}.");
                }
                
                fsm.EnterState(procedureID);
                
                OnEnterProcedureEvent?.Invoke(procedureID);
            }).Forget();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ExitProcedureImmediately(string procedureID)
        {
            ExitProcedureImmediately(procedureID, () => { });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ExitProcedureImmediately(string procedureID, Action onExit)
        {
            if (isLoading)
            {
                Debug.LogWarning("ProcedureManager is still loading, cannot switch procedure.");
                return;
            }
            
            if (fsm.HasCurrentState(procedureID) == false)
            {
                Debug.LogWarning($"Procedure with ID:{procedureID} is not current state.");
                return;
            }

            if (fsm.CanExitState(procedureID) == false)
            {
                Debug.LogError($"Failed to exit procedure with ID:{procedureID}.");
                return;
            }

            StartLoading(procedureID, ProcedureLoadingType.OnExit, () =>
            {
                if (fsm.CanExitState(procedureID) == false)
                {
                    throw new InvalidOperationException($"Failed to exit procedure with ID:{procedureID}.");
                }

                fsm.ExitState(procedureID);
                
                OnExitProcedureEvent?.Invoke(procedureID);
                
                onExit();
            }).Forget();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedure(string procedureID)
        {
            if (isLoading)
            {
                Debug.LogWarning("ProcedureManager is still loading, cannot switch procedure.");
                return;
            }
            
            if (_procedures.ContainsKey(procedureID) == false)
            {
                throw new ArgumentException($"Procedure with ID:{procedureID} does not exist.");
            }
            
            procedureSwitchQueue.Enqueue((null, procedureID));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedure(string fromProcedureID, string toProcedureID)
        {
            if (isLoading)
            {
                Debug.LogWarning("ProcedureManager is still loading, cannot switch procedure.");
                return;
            }
            
            if (_procedures.ContainsKey(fromProcedureID) == false)
            {
                throw new ArgumentException($"Procedure with ID:{fromProcedureID} does not exist.");
            }

            if (_procedures.ContainsKey(toProcedureID) == false)
            {
                throw new ArgumentException($"Procedure with ID:{toProcedureID} does not exist.");
            }

            procedureSwitchQueue.Enqueue((fromProcedureID, toProcedureID));
        }

        #endregion
    }
}