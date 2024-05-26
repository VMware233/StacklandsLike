using System;
using System.Runtime.CompilerServices;
using UnityEditor;

namespace VMFramework.OdinExtensions
{
    public enum ValidateType
    {
        Info,
        Warning,
        Error
    }

    public static class ValidateTypeUtility
    {
#if UNITY_EDITOR
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MessageType ToMessageType(this ValidateType validateType)
        {
            switch (validateType)
            {
                case ValidateType.Info:
                    return MessageType.Info;
                case ValidateType.Warning:
                    return MessageType.Warning;
                case ValidateType.Error:
                    return MessageType.Error;
                default:
                    throw new ArgumentOutOfRangeException(nameof(validateType),
                        validateType, null);
            }
        }
#endif
    }
}