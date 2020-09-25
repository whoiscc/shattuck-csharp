using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class Standard : IObject
    {
        private readonly IObject[] _storage;
        public IList<IObject> Storage => _storage;
        public ObjectLayout Layout { get; }

        public Standard(ObjectLayout layout)
        {
            _storage = new IObject[layout.StateMap.Count];
            Layout = layout;
        }
    }
}
