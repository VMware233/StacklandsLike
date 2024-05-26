using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static class BooleanUtility
    {
        #region To Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool value) => value ? 1 : 0;

        #endregion
    }
}
