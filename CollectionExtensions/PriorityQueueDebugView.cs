using System;

namespace CollectionExtensions
{
    internal sealed class PriorityQueueDebugView<T>
    {
        private readonly PriorityQueue<T> _queue;

        public PriorityQueueDebugView(PriorityQueue<T> queue)
        {
            _queue = queue;
        }

        public int Count
        {
            get { return _queue.Count; }
        }
    }
}
