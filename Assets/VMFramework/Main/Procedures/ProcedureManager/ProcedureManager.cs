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

        protected override void Awake()
        {
            base.Awake();

            Debug.Log("启动游戏！");
        }

        private void Start()
        {
            foreach (var derivedClass in typeof(IProcedure).GetDerivedClasses(false, false))
            {
                if (derivedClass.IsAbstract || derivedClass.IsInterface)
                {
                    continue;
                }

                var procedure = (IProcedure)derivedClass.CreateInstance();

                _procedures.Add(procedure.id, procedure);

                fsm.AddState(procedure);
            }

            fsm.Init(this);

            OnEnterProcedureEvent += procedureID => Debug.Log($"进入流程:{procedureID}");

            OnExitProcedureEvent += procedureID => Debug.Log($"离开流程:{procedureID}");

            ProcedureAutoSwitchBinder.RegisterBinding(VMFrameworkInitializationDoneProcedure.ID,
                GameInitializationDoneProcedure.ID);
            ProcedureAutoSwitchBinder.RegisterBinding(GameInitializationDoneProcedure.ID,
                MainMenuProcedure.ID);

            OnEnterProcedureEvent += procedureID =>
            {
                if (ProcedureAutoSwitchBinder.TryGetNextProcedureID(procedureID, out var nextProcedureID))
                {
                    EnterProcedure(nextProcedureID);
                }
            };

            CollectGameInitializers();

            EnterProcedure(VMFrameworkInitializationDoneProcedure.ID);
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
                }
                else
                {
                    ExitProcedureImmediately(fromProcedureID);
                }
            }
            
            EnterProcedureImmediately(toProcedureID);
        }

        #endregion

        #region Enter & Exit Procedure

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedureImmediately(string procedureID)
        {
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
            }).Forget();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedure(string procedureID)
        {
            if (_procedures.ContainsKey(procedureID) == false)
            {
                throw new ArgumentException($"Procedure with ID:{procedureID} does not exist.");
            }
            
            procedureSwitchQueue.Enqueue((null, procedureID));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnterProcedure(string fromProcedureID, string toProcedureID)
        {
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