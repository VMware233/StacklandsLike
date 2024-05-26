#if FISHNET
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using FishNet;
using FishNet.Managing.Timing;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public class PingDebugEntry : TitleContentDebugEntry
    {
        public const string ID = "ping_debug_entry";

        public override bool ShouldDisplay()
        {
            return InstanceFinder.IsClientStarted;
        }

        protected override string GetContent()
        {
            long ping = 0;
            TimeManager tm = InstanceFinder.TimeManager;
            if (tm != null)
            {
                ping = tm.RoundTripTime;
                var deduction = (long)(tm.TickDelta * 2000d);

                ping = (ping - deduction).Max(1);
            }

            return ping + "ms";
        }
    }
}

#endif