using System.Runtime.CompilerServices;
using VMFramework.GameEvents;

namespace VMFramework.UI
{
    public partial class UIPanelController
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Open(BoolInputGameEvent boolInputGameEvent)
        {
            this.Open();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Close(BoolInputGameEvent boolInputGameEvent)
        {
            this.Close();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Toggle(BoolInputGameEvent boolInputGameEvent)
        {
            this.Toggle();
        }
    }
}