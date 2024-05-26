#if FISHNET
using FishNet.Serializing;

namespace VMFramework.Property
{
    public static class BaseBoostIntPropertySerializer
    {
        public static void WriteBaseBoostIntProperty(this Writer writer, BaseBoostIntProperty property)
        {
            writer.WriteInt32(property.baseValue);
            writer.WriteSingle(property.boostValue);
        }

        public static BaseBoostIntProperty ReadBaseBoostIntProperty(this Reader reader)
        {
            var baseValue = reader.ReadInt32();
            var boostValue = reader.ReadSingle();
            return new BaseBoostIntProperty(baseValue, boostValue);
        }
    }
}
#endif