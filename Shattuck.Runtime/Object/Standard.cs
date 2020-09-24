using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class Standard : IObject
    {
        readonly IObject[] storage;
        public IList<IObject> Storage { get => storage; }
        public ObjectLayout Layout { get; }

        public Standard(ObjectLayout layout)
        {
            storage = new IObject[layout.StateMap.Count];
            Layout = layout;
        }
    }
}
