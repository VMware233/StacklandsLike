#if FISHNET
using FishNet.Serializing;
using UnityEngine.Scripting;

namespace VMFramework.Property
{
    [Preserve]
    public static class BaseBoostIntSerializer
    {
        public static void WriteBaseBoostInt(this Writer writer, BaseBoostInt value)
        {
            writer.WriteInt32(value.baseValue);
            writer.WriteSingle(value.boostValue);
        }
    
        public static BaseBoostInt ReadBaseBoostInt(this Reader reader)
        {
            int baseValue = reader.ReadInt32();
            float boostValue = reader.ReadSingle();
            return new BaseBoostInt(baseValue, boostValue);
        }
    }
}
#endif