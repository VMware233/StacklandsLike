namespace VMFramework.Core
{
    public interface IChooser
    {
        public object GetValue();
    }

    public interface IChooser<out T> : IChooser
    {
        public new T GetValue();

        object IChooser.GetValue()
        {
            return GetValue();
        }
    }
}