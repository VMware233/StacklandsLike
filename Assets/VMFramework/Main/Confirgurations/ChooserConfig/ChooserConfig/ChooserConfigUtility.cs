using System.Runtime.CompilerServices;

namespace VMFramework.Configuration
{
    public static class ChooserConfigUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegenerateObjectChooser(this IChooserConfig config)
        {
            config.RegenerateObjectChooser();
        }
    }
}