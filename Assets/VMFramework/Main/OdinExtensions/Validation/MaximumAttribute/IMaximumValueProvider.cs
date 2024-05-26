#if UNITY_EDITOR
namespace VMFramework.OdinExtensions
{
    public interface IMaximumValueProvider
    {
        public void ClampByMaximum(double maximum);
    }
}
#endif