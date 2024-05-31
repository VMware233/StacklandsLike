namespace VMFramework.Core
{
    public class GenericPriorityQueueNode<TPriority> : IGenericPriorityQueueNode<TPriority>
    {
        /// <summary>
        /// The Priority to insert this node at.
        /// Cannot be manually edited - see queue.Enqueue() and queue.UpdatePriority() instead
        /// </summary>
        public TPriority Priority { get; private set; }

        /// <summary>
        /// Represents the current position in the queue
        /// </summary>
        public int QueueIndex { get; private set; }

        /// <summary>
        /// Represents the order the node was inserted in
        /// </summary>
        public long InsertionIndex { get; private set; }

        TPriority IGenericPriorityQueueNode<TPriority>.Priority
        {
            get => Priority;
            set => Priority = value;
        }

        int IGenericPriorityQueueNode<TPriority>.QueueIndex
        {
            get => QueueIndex;
            set => QueueIndex = value;
        }

        long IGenericPriorityQueueNode<TPriority>.InsertionIndex
        {
            get => InsertionIndex;
            set => InsertionIndex = value;
        }
    }
}
