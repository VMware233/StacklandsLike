using VMFramework.Core;

namespace VMFramework.Property
{
    public readonly struct BaseBoostInt
    {
        public readonly int baseValue;
        public readonly float boostValue;
        public readonly int value;
        
        public BaseBoostInt(int baseValue, float boostValue)
        {
            this.baseValue = baseValue;
            this.boostValue = boostValue;
            this.value = (baseValue * boostValue).Floor();
        }
        
        public BaseBoostInt(int baseValue, float boostValue, int value)
        {
            this.baseValue = baseValue;
            this.boostValue = boostValue;
            this.value = value;
        }
    }
}