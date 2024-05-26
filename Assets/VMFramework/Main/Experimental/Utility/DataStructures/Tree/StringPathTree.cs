using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public static class StringPathTree
    {
        public static StringPathTreeNode<TData> BuildPathTree<TData>(
            this string path, StringPathTreeNode<TData> parent = null)
        {
            var parts = path.Split('/', '\\');

            if (parts.Length == 0)
            {
                return null;
            }

            var root = new StringPathTreeNode<TData>(parts[0], parent);
            var current = root;
            for (var i = 1; i < parts.Length; i++)
            {
                current = new StringPathTreeNode<TData>(parts[i], current);
            }

            return root;
        }

        public static StringPathTreeNode<TData> AddToPathTree<TData>(this StringPathTreeNode<TData> root,
            string path)
        {
            var parts = path.Split('/', '\\');

            if (parts.Length == 0)
            {
                return root;
            }

            var current = root;
            int partIndex = 0;
            string part = parts[partIndex];

            if (current.pathPart == part)
            {
                while (partIndex < parts.Length)
                {
                    partIndex++;

                    part = parts[partIndex];

                    var child = current.GetChildren()
                        .FirstOrDefault(c => c.pathPart == part);

                    if (child == null)
                    {
                        break;
                    }

                    current = child;
                }
            }

            if (partIndex >= parts.Length)
            {
                return current;
            }

            var validPath = parts.Skip(partIndex).Join("/");

            return BuildPathTree(validPath, current).GetAllLeaves(true)
                .FirstOrDefault();
        }

        public static StringPathTreeNode<TData> GetNodeFromPath<TData>(
            StringPathTreeNode<TData> root, string path)
        {
            var parts = path.Split('/', '\\');

            if (parts.Length == 0)
            {
                return null;
            }

            var current = root;
            foreach (var part in parts)
            {
                var child = current.GetChildren()
                    .FirstOrDefault(c => c.pathPart == part);

                if (child == null)
                {
                    return null;
                }

                current = child;
            }

            return current;
        }

        public static string GetPath<TData>(this StringPathTreeNode<TData> node)
        {
            var path = node.pathPart;

            foreach (var pathTreeNode in node.TraverseToRoot(false))
            {
                path = pathTreeNode.pathPart + "/" + path;
            }

            return path;
        }
    }

    public class StringPathTreeNode : StringPathTreeNode<object>
    {
        public StringPathTreeNode(string pathPart) : base(pathPart)
        {

        }

        public StringPathTreeNode(string pathPart, StringPathTreeNode<object> parent) : base(pathPart, parent)
        {

        }
    }

    public class StringPathTreeNode<TData> : ITreeNode<StringPathTreeNode<TData>>
    {
        public StringPathTreeNode<TData> parent { get; }

        [ShowInInspector]
        [EnableGUI]
        [PropertyOrder(1000)]
        private List<StringPathTreeNode<TData>> children { get; } = new();

        [ShowInInspector]
        [DisplayAsString]
        public string pathPart { get; }

        [ShowInInspector]
        public TData data;

        public StringPathTreeNode(string pathPart) : this(pathPart, null) { }

        public StringPathTreeNode(string pathPart, StringPathTreeNode<TData> parent)
        {
            this.pathPart = pathPart;

            this.parent = parent;
            if (parent != null)
            {
                parent.children.Add(this);
            }
        }

        #region Tree Node

        StringPathTreeNode<TData> IParentProvider<StringPathTreeNode<TData>>.GetParent() => parent;

        public IEnumerable<StringPathTreeNode<TData>> GetChildren() => children;

        #endregion
    }
}
