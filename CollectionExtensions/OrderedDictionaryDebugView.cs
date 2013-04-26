using System.Diagnostics;
using System.Linq;

namespace CollectionExtensions
{
    internal class OrderedDictionaryDebugView<TKey, TValue>
    {
        private readonly OrderedDictionary<TKey, TValue> _dictionary;

        public OrderedDictionaryDebugView(OrderedDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object Items
        {
            get
            {
                return _dictionary.ToArray();
            }
        }
    }
}
