using System.Diagnostics;
using System.Linq;

namespace CollectionExtensions
{
    internal class PropertyDictionaryDebugView
    {
        private readonly PropertyDictionary _dictionary;

        public PropertyDictionaryDebugView(PropertyDictionary dictionary)
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
