#if FISHNET
using FishNet.Serializing;

namespace VMFramework.Property
{
    public static class BaseBoostFloatPropertySerializer
    {
        public static void WriteBaseBoostFloatProperty(this Writer writer, BaseBoostFloatProperty property)
        {
            writer.WriteSingle(property.baseValue);
            writer.WriteSingle(property.boostValue);
        }

        public static BaseBoostFloatProperty ReadBaseBoostFloatProperty(this Reader reader)
        {
            var baseValue = reader.ReadSingle();
            var boostValue = reader.ReadSingle();
            return new BaseBoostFloatProperty(baseValue, boostValue);
        }
    }
}
#endif