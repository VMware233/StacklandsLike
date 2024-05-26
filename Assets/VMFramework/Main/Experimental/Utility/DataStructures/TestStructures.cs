using VMFramework.Core;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

[ManagerCreationProvider(ManagerType.TestingCore)]
public class TestStructures : SerializedMonoBehaviour
{
    public StringPathTreeNode<object> root;

    [Button]
    public void BuildPathTree(string path)
    {
        root = path.BuildPathTree<object>();
    }

    [Button]
    public void AddToPathTree(string path)
    {
        root.AddToPathTree(path);
    }
}
