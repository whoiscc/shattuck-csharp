using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class String : IObject
    {
        private static readonly TraitLayout NativeTrait = new TraitLayout(new Dictionary<string, uint>
        {
            {"print", 0},
        });

        private static readonly ObjectLayout AssociatedLayout =
            new ObjectLayout(new Dictionary<string, uint>());

        public ObjectLayout Layout => AssociatedLayout;
        public IList<IObject> Storage => new IObject[0];
        public string Native;

        public String(string rawString)
        {
            Native = rawString;
        }
    }
}