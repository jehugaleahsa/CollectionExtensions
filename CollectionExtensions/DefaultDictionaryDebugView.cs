using System.Diagnostics;
using System.Linq;

namespace CollectionExtensions
{
    internal class DefaultDictionaryDebugView<TKey, TValue>
    {
        private readonly DefaultDictionary<TKey, TValue> _dictionary;

        public DefaultDictionaryDebugView(DefaultDictionary<TKey, TValue> dictionary)
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
