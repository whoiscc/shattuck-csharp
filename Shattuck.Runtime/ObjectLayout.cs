using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shattuck.Runtime
{
    public class Method { }
    public class ObjectLayout
    {
        public ReadOnlyDictionary<string, uint> StateMap { get; }
        Dictionary<TraitLayout, Method[]> TraitMap { get; }
        public ObjectLayout(IDictionary<string, uint> stateMap)
        {
            StateMap = new ReadOnlyDictionary<string, uint>(stateMap);
            TraitMap = new Dictionary<TraitLayout, Method[]>();
        }
        public void AddImplementation(TraitLayout trait, IDictionary<string, Method> implementation)
        {
            var methods = new Method[implementation.Count];
            foreach (var item in implementation)
            {
                methods[trait.MethodMap[item.Key]] = item.Value;
            }
            TraitMap[trait] = methods;
        }
        public Method Dispatch(TraitLayout trait, string key)
        {
            return TraitMap[trait][trait.MethodMap[key]];
        }
    }

    public class TraitLayout
    {
        public ReadOnlyDictionary<string, uint> MethodMap { get; }
        public TraitLayout(IDictionary<string, uint> methodMap)
        {
            MethodMap = new ReadOnlyDictionary<string, uint>(methodMap);
        }
    }
}
