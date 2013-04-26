using System.Diagnostics;
using System.Linq;

namespace CollectionExtensions
{
    internal class ReadOnlySetDebugView<T>
    {
        private readonly ReadOnlySet<T> _set;

        public ReadOnlySetDebugView(ReadOnlySet<T> set)
        {
            _set = set;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object Items
        {
            get
            {
                return _set.ToArray();
            }
        }
    }
}
