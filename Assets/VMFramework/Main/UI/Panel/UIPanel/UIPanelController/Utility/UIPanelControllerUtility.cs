using System.Runtime.CompilerServices;

namespace VMFramework.UI
{
    public static partial class UIPanelControllerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Open(this IUIPanelController controller)
        {
            controller.Open(null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Open(this IUIPanelController controller, IUIPanelController source)
        {
            controller.Open(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Close(this IUIPanelController controller)
        {
            controller.Close(null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Close(this IUIPanelController controller, IUIPanelController source)
        {
            controller.Close(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Toggle(this IUIPanelController controller)
        {
            if (controller.isOpened)
            {
                controller.Close();
            }
            else
            {
                controller.Open();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destruct(this IUIPanelController controller)
        {
            controller.Destruct();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Crash(this IUIPanelController controller)
        {
            controller.Crash();
        }
    }
}