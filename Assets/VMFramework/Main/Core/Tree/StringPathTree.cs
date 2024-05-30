using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public sealed class StringPathTree<TData> : IChildrenProvider<StringPathTreeNode<TData>>, IEnumerable<StringPathTreeNode<TData>>
    {
        public StringPathTreeNode<TData> root { get; } = new(string.Empty);
        
        public string separator { get; init; } = "/";

        public void Add(string path, TData data)
        {
            var parts = path.Split(separator);
            
            var current = root;

            foreach (var part in parts)
            {
                if (part.IsNullOrEmpty())
                {
                    continue;
                }

                if (current.children.TryGetValue(part, out var child))
                {
                    current = child;
                    continue;
                }
                
                child = new StringPathTreeNode<TData>(part, current);
                current = child;
            }
            
            current.data = data;
        }

        public void Remove(string path)
        {
            var parts = path.Split(separator);

            var current = root;

            foreach (var part in parts)
            {
                if (part.IsNullOrEmpty())
                {
                    continue;
                }

                if (current.children.TryGetValue(part, out var child) == false)
                {
                    return;
                }
                
                current = child;
            }
            
            current.parent.RemoveChild(current.pathPart);
        }

        public IEnumerable<StringPathTreeNode<TData>> GetChildren()
        {
            return root.GetChildren();
        }

        public IEnumerator<StringPathTreeNode<TData>> GetEnumerator()
        {
            return root.GetAllLeaves(true).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public sealed class StringPathTreeNode<TData> : ITreeNode<StringPathTreeNode<TData>>
    {
        public StringPathTreeNode<TData> parent { get; }

        private readonly Dictionary<string, StringPathTreeNode<TData>> _children = new();

        public string pathPart { get; }

        public string folderPath
        {
            get
            {
                string path = null;

                foreach (var node in this.TraverseToRoot(false))
                {
                    if (path == null)
                    {
                        path = node.pathPart;
                    }
                    else
                    {
                        path = node.pathPart + "/" + path;
                    }
                }
                
                return path;
            }
        }

        public string fullPath
        {
            get
            {
                string path = pathPart;

                foreach (var node in this.TraverseToRoot(false))
                {
                    path = node.pathPart + "/" + path;
                }
                
                return path;
            }
        }

        public TData data;
        
        public IReadOnlyDictionary<string, StringPathTreeNode<TData>> children => _children;

        public StringPathTreeNode(string pathPart, StringPathTreeNode<TData> parent = null)
        {
            this.pathPart = pathPart;

            this.parent = parent;
            parent?._children.Add(pathPart, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveChild(string pathPart)
        {
            if (_children.Remove(pathPart) == false)
            {
                Debug.LogWarning($"{pathPart} not found in children of {this.pathPart}");
            }
        }

        #region Tree Node

        StringPathTreeNode<TData> IParentProvider<StringPathTreeNode<TData>>.GetParent() => parent;

        public IEnumerable<StringPathTreeNode<TData>> GetChildren() => _children.Values;

        #endregion
    }
}
