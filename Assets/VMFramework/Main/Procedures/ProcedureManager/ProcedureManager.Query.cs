using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    public partial class ProcedureManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasProcedure(string procedureID)
        {
            return _procedures.ContainsKey(procedureID);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasCurrentProcedure(string procedureID)
        {
            return fsm.HasCurrentState(procedureID);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProcedure GetProcedure(string procedureID)
        {
            return _procedures.GetValueOrDefault(procedureID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IProcedure GetProcedureStrictly(string procedureID)
        {
            if (_procedures.TryGetValue(procedureID, out var procedure) == false)
            {
                throw new ArgumentException($"Procedure with ID:{procedureID} does not exist.");
            }

            return procedure;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetProcedure(string procedureID, out IProcedure procedure)
        {
            return _procedures.TryGetValue(procedureID, out procedure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetProcedureWithWarning(string procedureID, out IProcedure procedure)
        {
            if (_procedures.TryGetValue(procedureID, out procedure) == false)
            {
                Debug.LogWarning($"Procedure with ID:{procedureID} does not exist.");
                return false;
            }
            
            return true;
        }
    }
}