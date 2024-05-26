using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core
{
    public class WeightedSelectChooser<T> : IChooser<T>
    {
        public T[] values { get; private set; }
        public float[] weights { get; private set; }
        
        public WeightedSelectChooser(T[] values, float[] weights)
        {
            Init(values, weights);
        }

        public WeightedSelectChooser(T[] values)
        {
            float[] weights = new float[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                weights[i] = 1f;
            }
            Init(values, weights);
        }

        public WeightedSelectChooser(IList<WeightedSelectItem<T>> items)
        {
            var values = new T[items.Count];
            var weights = new float[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                values[i] = items[i].value;
                weights[i] = items[i].weight;
            }
            Init(values, weights);
        }

        public WeightedSelectChooser(IEnumerable<IWeightedSelectItem<T>> items)
        {
            var itemsList = items.ToList();
            var values = new T[itemsList.Count];
            var weights = new float[itemsList.Count];
            for (int i = 0; i < itemsList.Count; i++)
            {
                values[i] = itemsList[i].value;
                weights[i] = itemsList[i].weight;
            }
            Init(values, weights);
        }

        public WeightedSelectChooser(IDictionary<T, float> itemDict)
        {
            var values = new T[itemDict.Count];
            var weights = new float[itemDict.Count];
            int i = 0;
            foreach (var item in itemDict)
            {
                values[i] = item.Key;
                weights[i] = item.Value;
                i++;
            }
            Init(values, weights);
        }

        private void Init(T[] values, float[] weights)
        {
            if (values.Length == 0)
            {
                Debug.LogError($"{nameof(values)} array is empty.");
                return;
            }
            
            if (weights.Length == 0)
            {
                Debug.LogError($"{nameof(weights)} array is empty.");
                return;
            }
            
            if (values.Length!= weights.Length)
            {
                Debug.LogError($"Length of {nameof(values)} and {nameof(weights)} arrays do not match.");
                return;
            }
            
            this.values = values;
            this.weights = weights;
        }

        public T GetValue()
        {
            return values.Choose(weights);
        }
    }
}