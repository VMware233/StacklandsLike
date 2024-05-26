using System;
using System.Collections;
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core.Linq;

public static class DBSCAN
{
    [Serializable]
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    public class Cluster<T> : ICollection<T>
        where T : struct, IEquatable<T>
    {
        [ShowInInspector]
        private HashSet<T> points = new();

        public void Add(T point)
        {
            points.Add(point);
        }

        public void Clear()
        {
            points.Clear();
        }

        public bool Contains(T item)
        {
            return points.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            points.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return points.Remove(item);
        }

        public int Count => points.Count;
        public bool IsReadOnly { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static IEnumerable<Cluster<Vector2Int>> Run(Vector2Int[] data, float epsilon, 
        int minPts, DistanceType distanceType)
    {
        var labels = new Cluster<Vector2Int>[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            if (labels[i] != null)
                continue;

            var neighbors = GetNeighbors(data, 0, i, distanceType, epsilon);

            if (neighbors.Count < minPts)
            {
                labels[i] = null; // mark as noise
                continue;
            }

            labels[i] = new();

            var queue = new Queue<int>(neighbors);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current <= i)
                {
                    continue;
                }

                if (labels[current] == null)
                {
                    labels[current] = labels[i];

                    var currentNeighbors = GetNeighbors(data, i, current,
                        distanceType, epsilon);
                    if (currentNeighbors.Count >= minPts)
                    {
                        foreach (var neighbor in currentNeighbors)
                            queue.Enqueue(neighbor);
                    }
                }
            }
        }

        foreach (var (index, cluster) in labels.Enumerate())
        {
            cluster.Add(data[index]);
        }

        var clusters = new HashSet<Cluster<Vector2Int>>(labels);

        return clusters;
    }

    private static List<int> GetNeighbors(Vector2Int[] data, int startIndex, int pointIndex, 
        DistanceType distanceType, float epsilon)
    {
        var neighbors = new List<int>();

        for (int i = startIndex; i < data.Length; i++)
        {
            if (data[pointIndex].Distance(data[i], distanceType) <= epsilon)
                neighbors.Add(i);
        }

        return neighbors;
    }
}
