using System.Diagnostics;
using System.Linq;

namespace CollectionExtensions
{
    internal class ReadOnlyDictionaryDebugView<TKey, TValue>
    {
        private readonly ReadOnlyDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionaryDebugView(ReadOnlyDictionary<TKey, TValue> dictionary)
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
