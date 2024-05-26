using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public readonly struct SingleValueChooser<T> : IChooser<T>
    {
        public readonly T value;
        
        public SingleValueChooser(T value)
        {
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue()
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        object IChooser.GetValue()
        {
            return value;
        }
    }
}