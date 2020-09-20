using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shattuck.Runtime
{
    public class ObjectLayout
    {
        public ObjectLayout(IDictionary<string, uint> stateMap, IDictionary<string, object> methodMap)
        {
            StateMap = new ReadOnlyDictionary<string, uint>(stateMap);
            MethodMap = new ReadOnlyDictionary<string, object>(methodMap);
        }

        public ReadOnlyDictionary<string, uint> StateMap { get; }
        public ReadOnlyDictionary<string, object> MethodMap { get; }
    }
}
