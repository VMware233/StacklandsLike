using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Core
{
    public class CircularSelectChooser<T> : IChooser<T>
    {
        public bool pingPong { get; }

        public int currentCircularIndex { get; private set; }

        public int currentCircularTimes { get; private set; }

        public bool loopForward { get; private set; } = true;

        private readonly CircularSelectItem<T>[] items;

        public IReadOnlyList<CircularSelectItem<T>> circularItems => items;

        public CircularSelectChooser(CircularSelectItem<T>[] items, bool pingPong = false,
            int startCircularIndex = 0)
        {
            this.items = items;
            this.pingPong = pingPong;
            currentCircularIndex = startCircularIndex;

            if (this.items.Length == 0)
            {
                Debug.LogError($"{nameof(CircularSelectChooser<T>)} has no items!");
                return;
            }

            if (this.items.Length == 1 && pingPong)
            {
                Debug.LogWarning(
                    $"{nameof(CircularSelectChooser<T>)} has only one item and ping-pong is enabled!");
            }
        }

        public CircularSelectChooser(IEnumerable<CircularSelectItem<T>> items, bool pingPong = false,
            int startCircularIndex = 0) : this(items.ToArray(), pingPong, startCircularIndex)
        {
        }

        public CircularSelectChooser(IEnumerable<T> items, bool pingPong = false, int startCircularIndex = 0)
            : this(items.Select(item => new CircularSelectItem<T>(item)), pingPong, startCircularIndex)
        {
        }

        public CircularSelectChooser(IEnumerable<ICircularSelectItem<T>> items, bool pingPong = false,
            int startCircularIndex = 0) : this(
            items.Select(item => new CircularSelectItem<T>(item.value, item.times)), pingPong,
            startCircularIndex)
        {
        }

        public T GetValue()
        {
            if (items.Length == 0)
            {
                return default;
            }

            var item = items[currentCircularIndex];

            if (pingPong == false)
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;
                    currentCircularIndex++;

                    if (currentCircularIndex >= items.Length)
                    {
                        currentCircularIndex = 0;
                    }
                }
            }
            else
            {
                currentCircularTimes++;
                if (currentCircularTimes > item.times)
                {
                    currentCircularTimes = 1;

                    if (loopForward)
                    {
                        currentCircularIndex++;

                        if (currentCircularIndex >= items.Length)
                        {
                            currentCircularIndex = items.Length - 2;
                            loopForward = false;
                        }
                    }
                    else
                    {
                        if (currentCircularIndex <= 0)
                        {
                            currentCircularIndex++;
                            loopForward = true;
                        }
                        else
                        {
                            currentCircularIndex--;
                        }
                    }
                }
            }

            return item.value;
        }
    }
}