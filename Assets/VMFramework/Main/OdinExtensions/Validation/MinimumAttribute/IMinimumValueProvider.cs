#if UNITY_EDITOR
namespace VMFramework.OdinExtensions
{
    public interface IMinimumValueProvider
    {
        public void ClampByMinimum(double minimum);
    }
}
#endif