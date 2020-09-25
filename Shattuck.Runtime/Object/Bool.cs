using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class Bool : IObject
    {
        public IList<IObject> Storage => new IObject[0];

        private static readonly ObjectLayout AssociatedLayout = 
            new ObjectLayout(new Dictionary<string, uint>());
        public ObjectLayout Layout => AssociatedLayout;

        public static readonly Bool True = new Bool(), False = new Bool();
    }
}