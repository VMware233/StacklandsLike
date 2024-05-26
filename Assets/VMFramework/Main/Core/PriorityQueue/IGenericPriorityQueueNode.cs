namespace VMFramework.Core
{
    public interface IGenericPriorityQueueNode<TPriority>
    {
        /// <summary>
        /// The Priority to insert this node at.
        /// Cannot be manually edited - see queue.Enqueue() and queue.UpdatePriority() instead
        /// Just explicit implement this property to make it clear that it is read-only
        /// </summary>
        public TPriority Priority { get; protected internal set; }

        /// <summary>
        /// Represents the current position in the queue
        /// Just explicit implement this property to make it clear that it is read-only
        /// </summary>
        public int QueueIndex { get; internal set; }

        /// <summary>
        /// Represents the order the node was inserted in
        /// Just explicit implement this property to make it clear that it is read-only
        /// </summary>
        public long InsertionIndex { get; internal set; }
    }
}