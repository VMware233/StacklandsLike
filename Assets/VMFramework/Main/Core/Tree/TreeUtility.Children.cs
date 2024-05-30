using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial class TreeUtility
    {
        #region Preorder Traverse

        #region Single Node

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreorderTraverse<T>(this T node,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return PreorderTraverse(node, includingSelf, node => node.GetChildren());
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreorderTraverse<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter)
        {
            if (includingSelf)
            {
                yield return node;
            }

            foreach (var child in childrenGetter(node))
            {
                foreach (var subNode in
                         PreorderTraverse(child, true, childrenGetter))
                {
                    yield return subNode;
                }
            }
        }

        /// <summary>
        /// 前序遍历，并返回指定类型的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> PreorderTraverse<T, TResult>(this T node,
            bool includingSelf)
            where T : class, IChildrenProvider<T> where TResult : T
        {
            return PreorderTraverse<T, TResult>(node, includingSelf,
                node => node.GetChildren());
        }

        /// <summary>
        /// 前序遍历，并返回指定类型的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> PreorderTraverse<T, TResult>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter) where TResult : T
        {
            foreach (var child in node.PreorderTraverse(includingSelf, childrenGetter))
            {
                if (child is TResult result)
                {
                    yield return result;
                }
            }
        }

        #endregion

        #region Mupltiple Nodes

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreorderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PreorderTraverse(includingSelf))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PreorderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf, Func<T, IEnumerable<T>> childrenGetter)
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PreorderTraverse(includingSelf,
                             childrenGetter))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// 前序遍历，并返回指定类型的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> PreorderTraverse<T, TResult>(
            this IEnumerable<T> nodes,
            bool includingSelf)
            where T : class, IChildrenProvider<T> where TResult : T
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PreorderTraverse<T, TResult>(
                             includingSelf))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// 前序遍历，并返回指定类型的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> PreorderTraverse<T, TResult>(
            this IEnumerable<T> nodes, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter) where TResult : T
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PreorderTraverse<T, TResult>(
                             includingSelf, childrenGetter))
                {
                    yield return child;
                }
            }
        }

        #endregion

        #endregion

        #region Postorder Traverse

        #region Single Node

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PostorderTraverse<T>(this T node,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return PostorderTraverse(node, includingSelf,
                node => node.GetChildren());
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PostorderTraverse<T>(this T node,
            bool includingSelf, Func<T, IEnumerable<T>> childrenGetter)
        {
            foreach (var child in childrenGetter(node))
            {
                foreach (var subNode in PostorderTraverse(child, true,
                             childrenGetter))
                {
                    yield return subNode;
                }
            }

            if (includingSelf)
            {
                yield return node;
            }
        }

        #endregion

        #region Multiple Nodes

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PostorderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PostorderTraverse(includingSelf))
                {
                    yield return child;
                }
            }
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> PostorderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf, Func<T, IEnumerable<T>> childrenGetter)
        {
            foreach (var node in nodes)
            {
                foreach (var child in node.PostorderTraverse(includingSelf,
                             childrenGetter))
                {
                    yield return child;
                }
            }
        }

        #endregion

        #endregion

        #region Level Order Traverse

        #region Single Node

        /// <summary>
        /// 层序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LevelOrderTraverse<T>(this T node,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return LevelOrderTraverse(node, includingSelf,
                node => node.GetChildren());
        }

        /// <summary>
        /// 层序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        public static IEnumerable<T> LevelOrderTraverse<T>(this T node,
            bool includingSelf, Func<T, IEnumerable<T>> childrenGetter)
        {
            var queue = new Queue<T>();

            if (includingSelf)
            {
                yield return node;
            }

            foreach (var subNode in childrenGetter(node))
            {
                queue.Enqueue(subNode);
            }

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                yield return currentNode;

                foreach (var child in childrenGetter(currentNode))
                {
                    queue.Enqueue(child);
                }
            }
        }

        #endregion

        #region Multiple Nodes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LevelOrderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return LevelOrderTraverse(nodes, includingSelf,
                node => node.GetChildren());
        }

        /// <summary>
        /// 层序遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="childrenGetter"></param>
        /// <returns></returns>
        public static IEnumerable<T> LevelOrderTraverse<T>(this IEnumerable<T> nodes,
            bool includingSelf, Func<T, IEnumerable<T>> childrenGetter)
        {
            var nodesList = nodes.ToList();

            var queue = new Queue<T>();

            if (includingSelf)
            {
                foreach (var node in nodesList)
                {
                    yield return node;
                }
            }

            foreach (var node in nodesList)
            {
                foreach (var subNode in childrenGetter(node))
                {
                    queue.Enqueue(subNode);
                }
            }

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                yield return currentNode;

                foreach (var child in childrenGetter(currentNode))
                {
                    queue.Enqueue(child);
                }
            }
        }

        #endregion

        #endregion

        #region Get All Children List

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> GetAllChildrenList<T>(this T node, bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return node.PreorderTraverse(includingSelf).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<T> GetAllChildrenList<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter)
        {
            return node.PreorderTraverse(includingSelf, childrenGetter).ToList();
        }

        #endregion

        #region Get All Leaves

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllLeaves<T>(this T node, bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return GetAllLeaves(node, includingSelf, node => node.GetChildren());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllLeaves<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter)
        {
            return node.PostorderTraverse(includingSelf, childrenGetter)
                .Where(child => childrenGetter(child).Any() == false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllLeaves<T>(this IEnumerable<T> nodes, bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            foreach (var node in nodes)
            {
                foreach (var leaf in node.GetAllLeaves(includingSelf))
                {
                    yield return leaf;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAllLeaves<T>(this IEnumerable<T> nodes,
            bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter)
        {
            foreach (var node in nodes)
            {
                foreach (var leaf in
                         node.GetAllLeaves(includingSelf, childrenGetter))
                {
                    yield return leaf;
                }
            }
        }

        #endregion

        #region Find Children

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> FindChildren<T>(this T node, bool includingSelf,
            Func<T, bool> predicate)
            where T : class, IChildrenProvider<T>
        {
            return PreorderTraverse(node, includingSelf).Where(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> FindChildren<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter, Func<T, bool> predicate)
        {
            return PreorderTraverse(node, includingSelf, childrenGetter).Where(predicate);
        }

        #endregion

        #region Find Child

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindChild<T>(this T node, bool includingSelf,
            Func<T, bool> predicate)
            where T : class, IChildrenProvider<T>
        {
            return PreorderTraverse(node, includingSelf).FirstOrDefault(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FindChild<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter, Func<T, bool> predicate)
        {
            return PreorderTraverse(node, includingSelf, childrenGetter)
                .FirstOrDefault(predicate);
        }

        #endregion

        #region Try Find Child

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFindChild<T>(this T node, bool includingSelf,
            Func<T, bool> predicate, out T result)
            where T : class, IChildrenProvider<T>
        {
            result = FindChild(node, includingSelf, predicate);
            return result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFindChild<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter, Func<T, bool> predicate,
            out T result)
        {
            result = FindChild(node, includingSelf, childrenGetter, predicate);
            return result != null;
        }

        #endregion

        #region Has Child

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasChild<T>(this T node, bool includingSelf,
            Func<T, bool> predicate)
            where T : class, IChildrenProvider<T>
        {
            return FindChildren(node, includingSelf, predicate).Any();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasChild<T>(this T node, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter, Func<T, bool> predicate)
        {
            return FindChildren(node, includingSelf, childrenGetter, predicate)
                .Any();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasChild<T>(this T node, T child, bool includingSelf)
            where T : class, IChildrenProvider<T>
        {
            return node.PreorderTraverse(includingSelf)
                .Any(node => node.Equals(child));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasChild<T>(this T node, T child, bool includingSelf,
            Func<T, IEnumerable<T>> childrenGetter)
            where T : class
        {
            return node.PreorderTraverse(includingSelf, childrenGetter)
                .Any(node => node.Equals(child));
        }

        #endregion

        #region Get Leaves Max Depth

        private readonly struct NodeDepthWrapper<TNode>
        {
            public int depth { get; init; }
            public TNode node { get; init; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetLeavesMaxDepth<T>(this T node)
            where T : class, IChildrenProvider<T>
        {
            return GetLeavesMaxDepth(node, node => node.GetChildren());
        }

        public static int GetLeavesMaxDepth<T>(this T node,
            Func<T, IEnumerable<T>> childrenGetter)
        {
            var children = childrenGetter(node).ToList();

            if (children.Any() == false)
            {
                return 0;
            }

            var queue = new Queue<NodeDepthWrapper<T>>();

            foreach (var leaf in children)
            {
                queue.Enqueue(new NodeDepthWrapper<T> { depth = 1, node = leaf });
            }

            var maxDepth = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                maxDepth = System.Math.Max(maxDepth, current.depth);

                foreach (var child in childrenGetter(current.node))
                {
                    queue.Enqueue(new NodeDepthWrapper<T>
                    {
                        depth = current.depth + 1,
                        node = child
                    });
                }
            }

            return maxDepth;
        }

        #endregion
    }
}