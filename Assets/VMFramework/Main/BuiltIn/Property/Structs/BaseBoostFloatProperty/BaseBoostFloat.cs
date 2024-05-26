
namespace VMFramework.Property
{
    public readonly struct BaseBoostFloat
    {
        public readonly float baseValue;
        public readonly float boostValue;
        public readonly float value;
        
        public BaseBoostFloat(float baseValue, float boostValue)
        {
            this.baseValue = baseValue;
            this.boostValue = boostValue;
            this.value = baseValue * boostValue;
        }

        public BaseBoostFloat(float baseValue, float boostValue, float value)
        {
            this.baseValue = baseValue;
            this.boostValue = boostValue;
            this.value = value;
        }
    }
}