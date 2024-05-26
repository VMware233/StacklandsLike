using VMFramework.Core;

namespace VMFramework.Configuration
{
    public interface ISingleChooserConfig<T> : IChooserConfig<T>
    {
        protected IChooser<T> objectChooser { get; set; }

        void IConfig.Init()
        {
            objectChooser = GenerateNewObjectChooser();
        }

        T IChooser<T>.GetValue()
        {
            return objectChooser.GetValue();
        }

        IChooser<T> IChooserConfig<T>.GetObjectChooser()
        {
            return objectChooser;
        }

        void IChooserConfig.RegenerateObjectChooser()
        {
            objectChooser = GenerateNewObjectChooser();
        }
    }
}