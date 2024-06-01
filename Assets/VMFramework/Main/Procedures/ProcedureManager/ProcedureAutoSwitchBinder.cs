using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Procedure
{
    /// <summary>
    /// This class is used to bind one procedure to another, so that when the first procedure is completed,
    /// the second procedure is automatically entered.
    /// </summary>
    public static class ProcedureAutoSwitchBinder
    {
        private static readonly Dictionary<string, string> autoSwitchBindings = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterBinding(string fromProcedureID, string toProcedureID)
        {
            if (autoSwitchBindings.TryAdd(fromProcedureID, toProcedureID))
            {
                return;
            }
            
            Debug.LogWarning($"Binding already exists for {fromProcedureID} to {toProcedureID}. " +
                             $"Overwriting with new binding : {fromProcedureID}->{toProcedureID}.");
            autoSwitchBindings[fromProcedureID] = toProcedureID;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnregisterBinding(string fromProcedureID, string toProcedureID)
        {
            if (autoSwitchBindings.TryGetValue(fromProcedureID, out string existingToProcedureID) == false)
            {
                Debug.LogWarning($"No binding exists for {fromProcedureID}");
                return;
            }

            if (existingToProcedureID != toProcedureID)
            {
                Debug.LogWarning($"Binding for {fromProcedureID}->{existingToProcedureID} " +
                                 $"does not match {fromProcedureID}->{toProcedureID}.");
                return;
            }
            
            autoSwitchBindings.Remove(fromProcedureID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnregisterBinding(string procedureID)
        {
            if (autoSwitchBindings.Remove(procedureID) == false)
            {
                Debug.LogWarning($"No binding exists for {procedureID}");
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetNextProcedureID(string currentProcedureID)
        {
            return autoSwitchBindings.GetValueOrDefault(currentProcedureID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetNextProcedureID(string currentProcedureID, out string nextProcedureID)
        {
            return autoSwitchBindings.TryGetValue(currentProcedureID, out nextProcedureID);
        }
    }
}