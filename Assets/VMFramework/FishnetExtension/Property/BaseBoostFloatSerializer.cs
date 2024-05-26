#if FISHNET
using FishNet.Serializing;
using UnityEngine.Scripting;

namespace VMFramework.Property
{
    [Preserve]
    public static class BaseBoostFloatSerializer
    {
        public static void WriteBaseBoostFloat(this Writer writer, BaseBoostFloat value)
        {
            writer.WriteSingle(value.baseValue);
            writer.WriteSingle(value.boostValue);
        }
    
        public static BaseBoostFloat ReadBaseBoostFloat(this Reader reader)
        {
            float baseValue = reader.ReadSingle();
            float boostValue = reader.ReadSingle();
            return new BaseBoostFloat(baseValue, boostValue);
        }
    }
}
#endif