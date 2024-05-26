using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public interface IContainerItem : IGameItem
    {
        public Container sourceContainer { get; set; }

        public int maxStackCount { get; }

        public int count { get; set; }

        public event Action<int, int> OnCountChangedEvent;

        public bool IsMergeableWith(IContainerItem other)
        {
            {
                if (other == null) return false;
                if (count >= maxStackCount) return false;

                return other.id == id;
            }
        }

        public void MergeWith(IContainerItem other)
        {
            if (other.count == 0) return;

            int maxIncrease = maxStackCount - count;

            if (maxIncrease > other.count)
            {
                count += other.count;
                other.count = 0;
            }
            else
            {
                count = maxStackCount;
                other.count -= maxIncrease;
            }
        }

        public bool IsSplittable(int targetCount)
        {
            return count > targetCount && count > 1;
        }

        public IContainerItem Split(int targetCount)
        {
            var clone = this.GetClone();

            clone.count = targetCount;

            count -= targetCount;

            return clone;
        }

        public void OnAddToContainer(Container container)
        {

        }

        public void OnRemoveFromContainer(Container container)
        {

        }
    }
}
