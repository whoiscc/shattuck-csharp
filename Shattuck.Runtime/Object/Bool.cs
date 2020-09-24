using System.Collections.Generic;

namespace Shattuck.Runtime.Object
{
    public class Bool : IObject
    {
        public IList<IObject> Storage { get => new IObject[0]; }
        
        static readonly ObjectLayout layout = new ObjectLayout(new Dictionary<string, uint>());
        public ObjectLayout Layout { get => layout; }

        public static Bool True = new Bool(), False = new Bool();
    }
}
