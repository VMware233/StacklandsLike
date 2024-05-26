namespace VMFramework.Core
{
    public interface ITreeNode<out T> : IParentProvider<T>, IChildrenProvider<T> 
        where T : class, ITreeNode<T>
    {

    }
}